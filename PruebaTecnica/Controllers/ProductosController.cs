using PruebaTecnica.Models;
using PruebaTecnica.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnica.Controllers
{
    public class ProductosController : Controller
    {

        public ActionResult Index()
        {
            Model1 context = new Model1();
            ProductoRepository productoRepository = new ProductoRepository(context);
            
            var productos = productoRepository.GetProductos();

            return View(productos);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Producto producto)
        {

            if(ModelState.IsValid)
            {
                Model1 context = new Model1();
                ProductoRepository productoRepository = new ProductoRepository(context);

                //var settingTiempoDevolucion = TimeSpan.FromMinutes(5); 
                //producto.TiempoDevolucion = producto.FechaIngreso.Add(settingTiempoDevolucion);

                productoRepository.AgregarProducto(producto);

                return RedirectToAction("Index");

            }
            return View(producto);
        }
    }
}