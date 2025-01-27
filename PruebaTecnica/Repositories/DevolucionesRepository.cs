using PruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PruebaTecnica.Repositories
{
    public class DevolucionesRepository
    {
        public readonly Model1 context;

        public DevolucionesRepository(Model1 context)
        {
            this.context  = context;
        }


     
        public bool AgregarDevolucion(Devolucione devolucion)
        {
            context.Devoluciones.Add(devolucion);
            context.SaveChanges();
            return true;
        }

       
        public IEnumerable<Devolucione> ObtenerDevolucionesPorProducto(int productoId)
        {
            return context.Devoluciones.Where(x => x.IdProducto == productoId).OrderByDescending(x => x.FechaDevolucion) .ToList();
        }

        
        public Devolucione ObtenerUltimaDevolucion(int productoId)
        {
            return context.Devoluciones.Where(x => x.IdProducto == productoId).OrderByDescending(x => x.FechaDevolucion)  .FirstOrDefault();
        }

        public bool ExisteUnaDevolucion(int productoId)
        {
            var Devolcion = context.Devoluciones.FirstOrDefault(x => x.IdProducto == productoId);

            if (Devolcion != null)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public void EditarEstadoDevolucion(int idProducto)
        {
            var productoBloqueado = context.Productoes.FirstOrDefault(x => x.IdProducto == idProducto);

            if (productoBloqueado != null)
            {
                productoBloqueado.Bloqueado = true;
                context.SaveChanges();
            }
        }
    }
}