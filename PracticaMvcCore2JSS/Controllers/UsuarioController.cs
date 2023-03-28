using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2JSS.Models;
using PracticaMvcCore2JSS.Repositories;
using PruebaParaExamen.Extensions;
using PruebaTiendaExamen.Filters;
using System.Security.Claims;

namespace PracticaMvcCore2JSS.Controllers
{
    public class UsuarioController : Controller
    {
        private RepositoryUsuario repo;
        private RepositoryLibros repoLibros;
        public UsuarioController(RepositoryUsuario repo, RepositoryLibros repoLibros)
        {
            this.repo = repo;
            this.repoLibros = repoLibros;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        public async Task<IActionResult> VerCarrito()
        {
            List<Libro> carrito;
            if (HttpContext.Session.GetObject<List<int>>("IDSLIBROSCARRITO") == null)
            {
                carrito = new List<Libro>();
            }
            else
            {
                List<int>idsLibrosCarrito = HttpContext.Session.GetObject<List<int>>("IDSLIBROSCARRITO");
                carrito = new List<Libro>();
                foreach (int id in idsLibrosCarrito)
                {
                    Libro lib = await this.repoLibros.GetLibroId(id);
                    carrito.Add(lib);
                }
                
            }
            return View(carrito);
        }
        [AuthorizeUsuarios]
        public async Task<IActionResult> VerPerfil()
        {
            string id =
             HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Usuario user =
               await this.repo.FindUsuario(int.Parse(id));
            return View(user);
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> VerPedidos()
        {
            string id =
             HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<VistaPedidos> pedidos=await this.repo.GetPedidosUser(int.Parse(id));
            return View(pedidos);
        }
    }
}
