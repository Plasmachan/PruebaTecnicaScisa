using PruebaTecnica.Models;
using PruebaTecnica.Models.ViewModels;
using PruebaTecnica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnica.Controllers
{
    public class OfertasController : Controller
    {
        // GET: Ofertas
        public ActionResult Index()
        {
            Model1 context = new Model1();
            ProductoRepository productoRepository = new ProductoRepository(context);
            OfertasRepository OfertasRepository = new OfertasRepository(context);
            DevolucionesRepository devolucionesRepository = new DevolucionesRepository(context);
           
            
            var productos = productoRepository.GetProductos();

            List<ProductosOfertadosViewModel> viewModels = new List<ProductosOfertadosViewModel>();


            foreach (var producto in productos)
            {
                ProductosOfertadosViewModel vm = new ProductosOfertadosViewModel(producto)
                {
                    Tiene3Ofertas = OfertasRepository.TieneMaximoDeOfertas(producto.IdProducto),
                    OfertaMayor = OfertasRepository.ObtenerOfertaMayor(producto.IdProducto),
                    TieneDevolcion = devolucionesRepository.ExisteUnaDevolucion(producto.IdProducto)
                };
                viewModels.Add(vm);
            }
            return View(viewModels);
        }



        [HttpPost]
        public JsonResult VerificarOfertas(int id)
        {
            Model1 context = new Model1();
            OfertasRepository OfertasRepository = new OfertasRepository(context);
            ProductoRepository ProductoRepository = new ProductoRepository(context);


            var TieneMasDe3Ofertas = OfertasRepository.TieneMaximoDeOfertas(id);
            var producto = ProductoRepository.GetProductoById(id);

            if (producto == null)
            {
                return Json(new { success = false, message = "El Producto No Existe" });
            }

            if (TieneMasDe3Ofertas == true)
            {
                return Json(new { success = false, message = "Ya se han recibido 3 ofertas para este producto. No se pueden aceptar más ofertas." });
            }

           return Json(new { success = true,  redirectUrl = Url.Action("CrearOferta", "Ofertas", new { id = id }) }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult CrearOferta(int id)
        {
            Model1 context = new Model1();
            OfertasRepository OfertasRepository = new OfertasRepository(context);
            ProductoRepository ProductoRepository = new ProductoRepository(context);


            var TieneMasDe3Ofertas = OfertasRepository.TieneMaximoDeOfertas(id);
            var producto = ProductoRepository.GetProductoById(id);

            if (producto == null)
            {
                TempData["ErrorMessage"] = "El Producto No Existe";
                return RedirectToAction("Index", "Ofertas");
            }

            if (TieneMasDe3Ofertas == true)
            {
                TempData["ErrorMessage"] = "Ya se han recibido 3 ofertas para este producto. No se pueden aceptar más ofertas.";
                return RedirectToAction("Index", "Ofertas");
            }

            OfertasViewModel vm = new OfertasViewModel();
            {
                vm.Idproducto = id;
                vm.oferta = new Oferta();
            };

            return View(vm);
        }


        [HttpPost]
        public ActionResult CrearOferta(OfertasViewModel ofertasviewmodel)
        {
            Model1 context = new Model1();
            OfertasRepository OfertasRepository = new OfertasRepository(context);

            if (ModelState.IsValid)
            {
                ofertasviewmodel.oferta.IdProducto = ofertasviewmodel.Idproducto;
                ofertasviewmodel.oferta.FechaOferta = DateTime.Now;
                OfertasRepository.AgregarOferta(ofertasviewmodel.oferta);
            }



            return RedirectToAction("Index");

        }



        public ActionResult VerOferta(int id)
        {

            Model1 context = new Model1();
            OfertasRepository OfertasRepository = new OfertasRepository(context);
            ProductoRepository ProductoRepository = new ProductoRepository(context);


            
            var producto = ProductoRepository.GetProductoById(id);

            if (producto == null)
            {
                TempData["ErrorMessage"] = "El Producto No Existe";
            }


            //obtener ofertas por producto
            var ofertas = OfertasRepository.ObtenerOfertasPorProducto(id);

           

            return View(ofertas);
        }




    }
}