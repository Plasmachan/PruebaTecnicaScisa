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