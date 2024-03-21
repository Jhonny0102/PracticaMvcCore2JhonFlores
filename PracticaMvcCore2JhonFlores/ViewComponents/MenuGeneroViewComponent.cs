using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2JhonFlores.Models;
using PracticaMvcCore2JhonFlores.Repositories;

namespace PracticaMvcCore2JhonFlores.ViewComponents
{
    public class MenuGeneroViewComponent : ViewComponent
    {
        private RepositoryLibros repo;
        public MenuGeneroViewComponent(RepositoryLibros repo)
        {
            this.repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos =await this.repo.GetAllGenerosAsync();
            return View(generos);
        }
    }
}
