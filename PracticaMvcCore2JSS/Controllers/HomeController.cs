using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2JSS.Models;
using PracticaMvcCore2JSS.Repositories;
using System.Diagnostics;

namespace PracticaMvcCore2JSS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RepositoryLibros repo;
        public HomeController(ILogger<HomeController> logger, RepositoryLibros repo)
        {
            _logger = logger;
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> _MostrarLibros()
        {
            List<Libro> libros =await this.repo.GetLibrosAsync();
            return PartialView(libros);
        }

      
        public async Task<IActionResult> _MostrarLibrosGenero(int idgenero)
        {
            List<Libro> libros = await this.repo.GetLibrosPorGeneroAsync(idgenero);
            return PartialView(libros);
        }
        public async Task<IActionResult> _LibrosPaginados(int? pos)
        {
            List<Libro> libros;
            LibrosPag pag;

            int cnt = 5;
            if (pos == null)
            {
                pag = await this.repo.GetLibrosPagAsync(0, cnt);
                ViewData["POSICION"] = 0;
            }
            else
            {
                pag = await this.repo.GetLibrosPagAsync(pos.Value, cnt);
                ViewData["POSICION"] = pos.Value;
            }
            ViewData["REGISTROS"] = pag.Total;
            libros = pag.Libros;
            return PartialView(libros);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}