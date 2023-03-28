using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2JSS.Models;
using PracticaMvcCore2JSS.Repositories;
using PruebaParaExamen.Extensions;
using PruebaTiendaExamen.Filters;
using System.Security.Claims;

namespace PracticaMvcCore2JSS.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryLibros repo;
        private RepositoryUsuario repoUser;
        public LibrosController(RepositoryLibros repo, RepositoryUsuario repoUser)
        {
            this.repo = repo;
            this.repoUser = repoUser;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult>Detalles(int idLibro)
        {
            Libro libro = await this.repo.GetLibroId(idLibro);
            return View(libro);
        }
        public async Task<IActionResult> AddLibroCarrito(int idLibro)
        {
            List<int> idsLibrosCarrito; 
            if (HttpContext.Session.GetObject<List<int>>("IDSLIBROSCARRITO") == null)
            {
                idsLibrosCarrito = new List<int>();
            }
            else
            {
                idsLibrosCarrito = HttpContext.Session.GetObject<List<int>>("IDSLIBROSCARRITO");
            }

            idsLibrosCarrito.Add(idLibro);
            HttpContext.Session.SetObject("IDSLIBROSCARRITO", idsLibrosCarrito);

            return RedirectToAction("Index", "Home");
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> HacerPedido()
        {
            List<Libro> carrito;
            if (HttpContext.Session.GetObject<List<int>>("IDSLIBROSCARRITO") == null)
            {
                return RedirectToAction("Home", "Index");


            }
            else
            {
                List<int> idsLibrosCarrito = HttpContext.Session.GetObject<List<int>>("IDSLIBROSCARRITO");
                carrito = new List<Libro>();
                foreach (int id in idsLibrosCarrito)
                {
                    Libro lib = await this.repo.GetLibroId(id);
                    carrito.Add(lib);
                }

                string iduser =
            HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Usuario user =
                   await this.repoUser.FindUsuario(int.Parse(iduser));
                DateTime hoy = DateTime.Today;
                string fechaHoy = hoy.Year + "-" + hoy.Month + "-" + hoy.Day;
                foreach (Libro lib in carrito)
                {
                    await this.repo.HacerPedido(user.IdUsuario, 1, hoy, lib.IdLibro, 1);
                }
                HttpContext.Session.SetObject("IDSLIBROSCARRITO", new List<int>());
                return RedirectToAction("VerPedidos", "Usuario");
            }

           
        }
    }
}
