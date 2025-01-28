using PruebaTecnica.Models;
using PruebaTecnica.Models.ViewModels;
using PruebaTecnica.Repositories;
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

        [Authorize]
        public ActionResult Index()
        {
            Model1 context = new Model1();
            ProductoRepository productoRepository = new ProductoRepository(context);
            OfertasRepository OfertasRepository = new OfertasRepository(context);
            DevolucionesRepository devolucionesRepository = new DevolucionesRepository(context);
            UsuarioRepository usuarioRepository = new UsuarioRepository(context);


            
           


            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var valorvacio = claims.FirstOrDefault();

            if (valorvacio.Value == "")
            {
                return RedirectToAction("Error", "Administrador");
            }
            
         
            var PrimeraPosicion = claims.FirstOrDefault(x => x.Type == "Id");
            var Id = PrimeraPosicion.Value;
            var Segunda = claims.FirstOrDefault(x => x.Type == "Name");
            var UserNombre = Segunda.Value;
            var Tercera = claims.FirstOrDefault(x => x.Type == "Role");
            var Role = Tercera.Value;


            var UsuarioLogueado = usuarioRepository.GetUsuarioByName(UserNombre);

            if (UsuarioLogueado != null)
            {
                if(UsuarioLogueado.TipoUsuario ==  Role)
                {
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
            }
            else
            {
                return RedirectToAction("Error", "Administrador");
            }
           
            return View();
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }


        [HttpGet]
        [Authorize]
        public ActionResult Crear2()
        {
            Model1 model1 = new Model1();
            UsuarioRepository usuarioRepository = new UsuarioRepository(model1);
            ProductoRepository productoRepository = new ProductoRepository(model1);

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var valorvacio = claims.FirstOrDefault();

            if (valorvacio.Value == "")
            {
                return RedirectToAction("Error", "Administrador");
            }

            var PrimeraPosicion = claims.FirstOrDefault(x => x.Type == "Id");
            var Id = PrimeraPosicion.Value;
            var Segunda = claims.FirstOrDefault(x => x.Type == "Name");
            var UserNombre = Segunda.Value;
            var Tercera = claims.FirstOrDefault(x => x.Type == "Role");
            var Role = Tercera.Value;
            var UsuarioLogueado = usuarioRepository.GetUsuarioByName(UserNombre);

            if (UsuarioLogueado != null)
            {
                if (UsuarioLogueado.TipoUsuario == Role)
                {
                    return View();
                }

            }
            return View();         
        }

        [HttpPost]
        public ActionResult Crear2(Producto producto)
        {

            Model1 model1 = new Model1();
            UsuarioRepository usuarioRepository = new UsuarioRepository(model1);
            ProductoRepository productoRepository = new ProductoRepository(model1);


            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var valorvacio = claims.FirstOrDefault();

            if (valorvacio.Value == "")
            {
                return RedirectToAction("Error", "Administrador");
            }

            var PrimeraPosicion = claims.FirstOrDefault(x => x.Type == "Id");
            var Id = PrimeraPosicion.Value;
            var Segunda = claims.FirstOrDefault(x => x.Type == "Name");
            var UserNombre = Segunda.Value;
            var Tercera = claims.FirstOrDefault(x => x.Type == "Role");
            var Role = Tercera.Value;
            var UsuarioLogueado = usuarioRepository.GetUsuarioByName(UserNombre);



            if (UsuarioLogueado != null)
            {
                if (UsuarioLogueado.TipoUsuario == Role)
                {
                    if (ModelState.IsValid)
                    {                      
                        productoRepository.AgregarProducto(producto);
                        return RedirectToAction("Index", "Administrador");

                    }
                }

                else
                {
                    RedirectToAction("Error", "Administrador");
                }
            }

          
            return View(producto);
        }


        [Authorize]
        public ActionResult Ofertas()
        {
            Model1 context = new Model1();
            ProductoRepository productoRepository = new ProductoRepository(context);
            OfertasRepository OfertasRepository = new OfertasRepository(context);
            DevolucionesRepository devolucionesRepository = new DevolucionesRepository(context);
            var productos = productoRepository.GetProductos();
            UsuarioRepository usuarioRepository = new UsuarioRepository(context);
            List<ProductosOfertadosViewModel> viewModels = new List<ProductosOfertadosViewModel>();


            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var valorvacio = claims.FirstOrDefault();

            if (valorvacio.Value == "")
            {
                return RedirectToAction("Error", "Administrador");
            }

            var PrimeraPosicion = claims.FirstOrDefault(x => x.Type == "Id");
            var Id = PrimeraPosicion.Value;
            var Segunda = claims.FirstOrDefault(x => x.Type == "Name");
            var UserNombre = Segunda.Value;
            var Tercera = claims.FirstOrDefault(x => x.Type == "Role");
            var Role = Tercera.Value;
            var UsuarioLogueado = usuarioRepository.GetUsuarioByName(UserNombre);


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

            if (UsuarioLogueado != null)
            {
                if (UsuarioLogueado.TipoUsuario == Role)
                {
                    if (ModelState.IsValid)
                    {
                        return View(viewModels);

                    }
                }

                else
                {
                    RedirectToAction("Error", "Administrador");
                }
            }


            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult VerOfertas(int id)
        {
            Model1 context = new Model1();
            ProductoRepository productoRepository = new ProductoRepository(context);
            OfertasRepository OfertasRepository = new OfertasRepository(context);
            DevolucionesRepository devolucionesRepository = new DevolucionesRepository(context);
            UsuarioRepository usuarioRepository = new UsuarioRepository(context);

            var OfertasPorProducto = OfertasRepository.ObtenerOfertasPorProducto(id);

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var valorvacio = claims.FirstOrDefault();

            if (valorvacio.Value == "")
            {
                return RedirectToAction("Error", "Administrador");
            }

            var PrimeraPosicion = claims.FirstOrDefault(x => x.Type == "Id");
            var Id = PrimeraPosicion.Value;
            var Segunda = claims.FirstOrDefault(x => x.Type == "Name");
            var UserNombre = Segunda.Value;
            var Tercera = claims.FirstOrDefault(x => x.Type == "Role");
            var Role = Tercera.Value;
            var UsuarioLogueado = usuarioRepository.GetUsuarioByName(UserNombre);

            if (UsuarioLogueado != null)
            {
                if (UsuarioLogueado.TipoUsuario == Role)
                {
                    if (ModelState.IsValid)
                    {
                        return View(OfertasPorProducto);
                      

                    }
                }

                else
                {
                    RedirectToAction("Error", "Administrador");
                }
            }

            return View();

        }



        [HttpGet]
        [Authorize]
        public ActionResult OfertaGanadora(int id)
        {

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            

            Model1 context = new Model1();
            OfertasRepository OfertasRepository = new OfertasRepository(context);


            var oferta = OfertasRepository.GetOferta(id);
            return View(oferta);
        }


        [HttpPost]
        public ActionResult OfertaGanadora(Oferta oferta)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;



            Model1 context = new Model1();
            OfertasRepository OfertasRepository = new OfertasRepository(context);
            ProductoRepository productoRepository = new ProductoRepository(context);

            var idProducto = oferta.IdProducto;

            var producto = productoRepository.GetProductoById(idProducto);

            if (ModelState.IsValid)
            {
                var listaOfertas = OfertasRepository.ObtenerOfertasPorProducto(idProducto);

                foreach (var item in listaOfertas)
                {
                    OfertasRepository.EliminarOferta(item);
                }
                productoRepository.EliminarProducto(producto);
               

            }



            return RedirectToAction("Ofertas", "Administrador");

        }



        [HttpGet]
        [Authorize]
        public ActionResult Editar(int id)
        {


            Model1 context = new Model1();
            ProductoRepository productoRepository = new ProductoRepository(context);
            UsuarioRepository usuarioRepository = new UsuarioRepository(context);

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var valorvacio = claims.FirstOrDefault();

            if (valorvacio.Value == "")
            {
                return RedirectToAction("Error", "Administrador");
            }

            var PrimeraPosicion = claims.FirstOrDefault(x => x.Type == "Id");
            var Id = PrimeraPosicion.Value;
            var Segunda = claims.FirstOrDefault(x => x.Type == "Name");
            var UserNombre = Segunda.Value;
            var Tercera = claims.FirstOrDefault(x => x.Type == "Role");
            var Role = Tercera.Value;
            var UsuarioLogueado = usuarioRepository.GetUsuarioByName(UserNombre);

            if (UsuarioLogueado != null)
            {
                if (UsuarioLogueado.TipoUsuario == Role)
                {
                    if (ModelState.IsValid)
                    {

                        var producto = productoRepository.GetProductoById(id);
                        return View(producto);


                    }
                }

                else
                {
                    RedirectToAction("Error", "Administrador");
                }
            }

            return View();

        }


        [HttpPost]
        public ActionResult Editar(Producto p)
        {

            Model1 context = new Model1();
            ProductoRepository productoRepository = new ProductoRepository(context);
            UsuarioRepository usuarioRepository = new UsuarioRepository(context);


            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var valorvacio = claims.FirstOrDefault();
            if (valorvacio.Value == "")
            {
                return RedirectToAction("Error", "Administrador");
            }

            var PrimeraPosicion = claims.FirstOrDefault(x => x.Type == "Id");
            var Id = PrimeraPosicion.Value;
            var Segunda = claims.FirstOrDefault(x => x.Type == "Name");
            var UserNombre = Segunda.Value;
            var Tercera = claims.FirstOrDefault(x => x.Type == "Role");
            var Role = Tercera.Value;
            var UsuarioLogueado = usuarioRepository.GetUsuarioByName(UserNombre);


            if (UsuarioLogueado != null)
            {
                if (UsuarioLogueado.TipoUsuario == Role)
                {
                    if (ModelState.IsValid)
                    {



                        productoRepository.Editar(p);
                        return RedirectToAction("Index", "Administrador");


                    }
                }

                else
                {
                    RedirectToAction("Error", "Administrador");
                }
            }

            return View();
        }


        [HttpGet]
        [Authorize]
        public ActionResult Eliminar(int id)
        {


            Model1 context = new Model1();
            ProductoRepository productoRepository = new ProductoRepository(context);
            UsuarioRepository usuarioRepository = new UsuarioRepository(context);

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var valorvacio = claims.FirstOrDefault();

            if (valorvacio.Value == "")
            {
                return RedirectToAction("Error", "Administrador");
            }

            var PrimeraPosicion = claims.FirstOrDefault(x => x.Type == "Id");
            var Id = PrimeraPosicion.Value;
            var Segunda = claims.FirstOrDefault(x => x.Type == "Name");
            var UserNombre = Segunda.Value;
            var Tercera = claims.FirstOrDefault(x => x.Type == "Role");
            var Role = Tercera.Value;
            var UsuarioLogueado = usuarioRepository.GetUsuarioByName(UserNombre);

            if (UsuarioLogueado != null)
            {
                if (UsuarioLogueado.TipoUsuario == Role)
                {
                    if (ModelState.IsValid)
                    {

                        var producto = productoRepository.GetProductoById(id);
                        return View(producto);


                    }
                }

                else
                {
                    RedirectToAction("Error", "Administrador");
                }
            }

            return View();

        }


        [HttpPost]
        public ActionResult Eliminar(Producto p)
        {

            Model1 context = new Model1();
            ProductoRepository productoRepository = new ProductoRepository(context);
            UsuarioRepository usuarioRepository = new UsuarioRepository(context);
            OfertasRepository ofertasRepository = new OfertasRepository(context);
            DevolucionesRepository devolucionesRepository = new DevolucionesRepository(context);


            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            var valorvacio = claims.FirstOrDefault();
            if (valorvacio.Value == "")
            {
                return RedirectToAction("Error", "Administrador");
            }

            var PrimeraPosicion = claims.FirstOrDefault(x => x.Type == "Id");
            var Id = PrimeraPosicion.Value;
            var Segunda = claims.FirstOrDefault(x => x.Type == "Name");
            var UserNombre = Segunda.Value;
            var Tercera = claims.FirstOrDefault(x => x.Type == "Role");
            var Role = Tercera.Value;
            var UsuarioLogueado = usuarioRepository.GetUsuarioByName(UserNombre);

            var producto = productoRepository.GetProductoById(p.IdProducto);

            if (UsuarioLogueado != null)
            {
                if (UsuarioLogueado.TipoUsuario == Role)
                {
                    var listaOfertas = ofertasRepository.ObtenerOfertasPorProducto(producto.IdProducto);
                    var ListaDevolucion = devolucionesRepository.ObtenerDevolucionesPorProducto(producto.IdProducto);

                    foreach (var item in listaOfertas)
                    {
                        ofertasRepository.EliminarOferta(item);
                    }

                    devolucionesRepository.Eliminar(ListaDevolucion);
                    

                    productoRepository.EliminarProducto(producto);
                        return RedirectToAction("Index", "Administrador");
                }

                else
                {
                    RedirectToAction("Error", "Administrador");
                }
            }

            return View();
        }






    }
}