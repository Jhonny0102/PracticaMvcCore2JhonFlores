using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2JhonFlores.Extensions;
using PracticaMvcCore2JhonFlores.Filters;
using PracticaMvcCore2JhonFlores.Models;
using PracticaMvcCore2JhonFlores.Repositories;

namespace PracticaMvcCore2JhonFlores.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryLibros repo;
        public LibrosController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index(int? idgenero)
        {
            if (idgenero != null)
            {
                List<Libro> libros = await this.repo.GetLibrosGeneroAsync(idgenero.Value);
                return View(libros);
            }
            else
            {
                List<Libro> libros = await this.repo.GetAllLibrosAsync();
                return View(libros);
            }
        }
        
        public async Task<IActionResult> DetailsLibro(int idlibro)
        {
            Libro milibro = await this.repo.FindLibroAsync(idlibro);
            return View(milibro);
        }

        [AuthorizeUsers]
        public IActionResult PerfilUsuario()
        {
            return View();
        }



        [AuthorizeUsers]
        public IActionResult GuardarLibro(int idlibro)
        {
            List<int> idsLibros;
            if (HttpContext.Session.GetString("IDSLIBROS") == null)
            {
                idsLibros = new List<int>();
            }
            else
            {
                idsLibros = HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
            }
            idsLibros.Add(idlibro);
            HttpContext.Session.SetObject("IDSLIBROS", idsLibros);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LibrosGuardamos(int? ideliminar)
        {
            List<int> idslibros = HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
            if (idslibros != null)
            {
                if (ideliminar != null)
                {
                    idslibros.Remove(ideliminar.Value);
                    if (idslibros.Count == 0)
                    {
                        HttpContext.Session.Remove("IDSLIBROS");
                    }
                    else
                    {
                        HttpContext.Session.SetObject("IDSLIBROS", idslibros);
                    }
                }
                List<Libro> mislibros = await this.repo.GetIdsLibrosAsync(idslibros); ;
                return View(mislibros);
            }
            return View();
        }

    }
}
