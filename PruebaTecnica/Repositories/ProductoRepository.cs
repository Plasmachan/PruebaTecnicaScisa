using PruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTecnica.Repositories
{
   
    public class ProductoRepository
    {

        public readonly Model1 context;

        public ProductoRepository(Model1 context)
        {
            this.context  = context;
        }


        public IEnumerable<Producto> GetProductos()
        {
            return context.Productoes.ToList();
        }

        public void AgregarProducto(Producto producto)
        {
            context.Productoes.Add(producto);
            context.SaveChanges();
        }

        public void Editar(Producto P)
        {
            var producto = context.Productoes.FirstOrDefault(x=>x.IdProducto == P.IdProducto);

            producto.NombreProducto = P.NombreProducto;
            producto.FechaIngreso = P.FechaIngreso;
            producto.TiempoDevolucion = P.TiempoDevolucion;
            context.SaveChanges();
        }



        public Producto GetProductoById(int Id)
        {
            return context.Productoes.FirstOrDefault(x=>x.IdProducto == Id);
        }

        public void EliminarProducto(Producto producto)
        {
          
                context.Productoes.Remove(producto); 
                context.SaveChanges();           
           
            
        }








    }
}