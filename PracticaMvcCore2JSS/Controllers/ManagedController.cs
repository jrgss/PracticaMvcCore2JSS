using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2JSS.Models;
using PracticaMvcCore2JSS.Repositories;
using System.Security.Claims;

namespace PracticaMvcCore2JSS.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryUsuario repo;
        public ManagedController(RepositoryUsuario repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(string email,string pass)
        {
            Usuario user = await this.repo.FindUsuario(email);
            if (user != null)
            {
                ClaimsIdentity identity =
                 new ClaimsIdentity
                 (CookieAuthenticationDefaults.AuthenticationScheme,
                 ClaimTypes.Name, ClaimTypes.Role);
                Claim claimName = new Claim(ClaimTypes.Name, user.Nombre);
                identity.AddClaim(claimName);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()) ;
                identity.AddClaim(claimId);
                Claim claimRole = new Claim(ClaimTypes.Role, "Comprador");
                identity.AddClaim(claimRole);
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync
                 (CookieAuthenticationDefaults.AuthenticationScheme
                 , userPrincipal);
                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                return RedirectToAction(action, controller);
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
