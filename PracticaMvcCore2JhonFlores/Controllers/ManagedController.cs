using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2JhonFlores.Repositories;
using System.Security.Claims;
using PracticaMvcCore2JhonFlores.Models;

namespace PracticaMvcCore2JhonFlores.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryLibros repo;
        public ManagedController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            
            Usuario usuario = await this.repo.FindusuarioAsync(email,password);
            if (usuario != null)
            {
                //Seguridad
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme,ClaimTypes.Email, ClaimTypes.SerialNumber);

                //Asignamos los claims
                Claim claimEmail = new Claim(ClaimTypes.Email, usuario.Email);
                identity.AddClaim(claimEmail);

                Claim claimPassword = new Claim(ClaimTypes.SerialNumber,usuario.Pass);
                identity.AddClaim(claimPassword);

                Claim claimRole = new Claim("ID", usuario.IdUsuario.ToString());
                identity.AddClaim(claimRole);

                Claim claimNombre = new Claim("NOMBRE", usuario.Nombre);
                identity.AddClaim(claimNombre);

                Claim claimApellido = new Claim("APELLIDO", usuario.Apellido);
                identity.AddClaim(claimApellido);

                Claim claimFoto = new Claim("FOTO", usuario.Foto);
                identity.AddClaim(claimFoto);


                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                //string controller = TempData["controller"].ToString();
                //string action = TempData["action"].ToString();
                return RedirectToAction("PerfilUsuario", "Libros");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario / Password incorrectos";
                return View();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Libros");
        }
    }
}
