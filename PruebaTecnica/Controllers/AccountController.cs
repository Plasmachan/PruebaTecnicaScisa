using PruebaTecnica.Helpers;
using PruebaTecnica.Models;
using PruebaTecnica.Models.ViewModels;
using PruebaTecnica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Specialized;


namespace PruebaTecnica.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        



        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel vm)
        {
            Model1 model1 = new Model1();
            UsuarioRepository usuarioRepository = new UsuarioRepository(model1);


            if (ModelState.IsValid)
            {
                // Validar el usuario (puedes hacer esto a través de un servicio)
                var usuario = usuarioRepository.ValidarUsuario(vm.Usuario, vm.Contraseña);
                if (usuario != null)
                {


                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("Id", usuario.IdUsuario.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, usuario.NombreUsuario));
                    claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Forms");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.User = claimsPrincipal;












                    var authTicket = new FormsAuthenticationTicket(
                        1,                             // version
                        usuario.NombreUsuario,                      // user name
                        DateTime.Now,                  // created
                        DateTime.Now.AddMinutes(20),   // expires
                        true,                    // persistent?
                        "Moderator;Admin"                        // can be used to store roles
                        );


                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
        

                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    FormsAuthentication.SetAuthCookie(User.Identity.Name, false);
                    System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);





                    // Redirigir a la página principal o área protegida
                    return RedirectToAction("Index", "Administrador");
                }
                else
                {
                    ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                }
            }

            return View();
        }


        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(SignUpViewModel vm)
        {

            Model1 model1 = new Model1();
            UsuarioRepository usuarioRepository = new UsuarioRepository(model1);

            if (ModelState.IsValid)
            {
                Usuario u = new Usuario();
                u.NombreUsuario = vm.Nombre;
                u.Contraseña = Encriptacion.GetMD5(vm.Contraseña);
                u.TipoUsuario = "Administrador";
                usuarioRepository.InsertarUsuario(u);
                return RedirectToAction("Login");
            }
            else
            {
                return View(vm);
            }
        }



        // Acción para cerrar sesión
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();  // Eliminar la cookie de autenticación
            return RedirectToAction("Login", "Account");
        }

    }
}