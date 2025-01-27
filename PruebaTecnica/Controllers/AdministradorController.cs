using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnica.Controllers
{
    public class AdministradorController : Controller
    {

        //[Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var CrearIdentity = HttpContext.User.Identity as ClaimsIdentity;


            return View();
        }
    }
}