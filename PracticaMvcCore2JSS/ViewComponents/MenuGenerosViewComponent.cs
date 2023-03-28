using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2JSS.Models;
using PracticaMvcCore2JSS.Repositories;

namespace PracticaMvcCore2JSS.ViewComponents
{
    public class MenuGenerosViewComponent : ViewComponent
    {
        private RepositoryLibros repo;
        public MenuGenerosViewComponent(RepositoryLibros repo)
        {
            this.repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos = await this.repo.GetGenerosAsync();
            return View(generos);
        }
    }
}
