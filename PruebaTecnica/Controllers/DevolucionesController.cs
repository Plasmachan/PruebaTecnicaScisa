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
    public class DevolucionesController : Controller
    {
        // GET: Devoluciones
        public ActionResult Index()
        {
            Model1 context = new Model1();
            ProductoRepository productoRepository = new ProductoRepository(context);
            OfertasRepository OfertasRepository = new OfertasRepository(context);
            var productos = productoRepository.GetProductos();
            return View(productos);
        }

        [HttpGet]
        public ActionResult CrearDevolucion(int id)
        {
            Model1 context = new Model1();
            ProductoRepository ProductoRepository = new ProductoRepository(context);
            var producto = ProductoRepository.GetProductoById(id);

            if (producto == null)
            {
                TempData["ErrorMessage"] = "El Producto No Existe";
                return RedirectToAction("Index", "Ofertas");
            }


            DevolucionViewModel vm = new DevolucionViewModel();
            {
                vm.Idproducto = id;
                vm.NombreDelProducto = producto.NombreProducto;
               
            };


            return View(vm);

        }


        [HttpPost]
        public JsonResult CrearDevolucion(int id, string comentario)
        {

            Model1 context = new Model1();
            DevolucionesRepository DevolucionesRepository = new DevolucionesRepository(context);
            ProductoRepository ProductoRepository = new ProductoRepository(context);

            

          
            var producto = ProductoRepository.GetProductoById(id);
            if (producto == null)
            {
                //Si no encontramos el producto, retornamos un mensaje de error
                return Json(new { success = false, message = "Producto no encontrado" });
            }


            var ExisteDevolucion = DevolucionesRepository.ExisteUnaDevolucion(id);
            if (ExisteDevolucion)
            {
                return Json(new { success = false, message = "Ya hubo una devolucion por el producto", redirectUrl = Url.Action("Index", "Devoluciones") });
            }


            if (DateTime.Now > producto.TiempoDevolucion)
            {
                DevolucionesRepository.EditarEstadoDevolucion(producto.IdProducto);
                return Json(new { success = false, message = "El tiempo para devolver el producto ha expirado. El producto ha sido bloqueado.", redirectUrl = Url.Action("Index", "Devoluciones") });
            }

          
            var devolucion = new Devolucione
            {
                IdProducto = id,
                ComentarioDevolucion = comentario,
                FechaDevolucion = DateTime.Now,
                EstadoDevolucion = true,
            };




           
            var mensaje = DevolucionesRepository.AgregarDevolucion(devolucion);
            DevolucionesRepository.EditarEstadoDevolucion(id);
          
            if (mensaje == true)
            {
                return Json(new { success = true, message = "Devolución registrada correctamente.", redirectUrl = Url.Action("Index", "Devoluciones") });
            }
            else
            {
                return Json(new { success = false, message = "Ocurrió un error al registrar la devolución." });
            }
        }

       
        private DateTime CalcularTiempoLimiteDevolucion(DateTime fechaIngreso, string tiempoLimite)
        {
            var tiempoLimiteSpan = TimeSpan.Parse(tiempoLimite);
            return fechaIngreso.Add(tiempoLimiteSpan);
        }
    }
}
