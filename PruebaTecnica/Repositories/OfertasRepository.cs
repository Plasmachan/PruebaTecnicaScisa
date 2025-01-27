using PruebaTecnica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTecnica.Repositories
{
    public class OfertasRepository
    {

        public readonly Model1 context;

        public OfertasRepository(Model1 context)
        {
            this.context  = context;
        }


        public void AgregarOferta(Oferta oferta)
        {
            context.Ofertas.Add(oferta);  
            context.SaveChanges(); 
        }

 
        public IEnumerable<Oferta> ObtenerOfertasPorProducto(int IdProducto)
        {
            return context.Ofertas.Where(x => x.IdProducto == IdProducto).OrderByDescending(x => x.FechaOferta).ToList();
        }

       
        public Oferta ObtenerOfertaMayor(int IdProducto)
        {
            return context.Ofertas.Where(x => x.IdProducto == IdProducto).OrderByDescending(x => x.MontoOferta).FirstOrDefault(); 
        }

       
        public bool TieneMaximoDeOfertas(int IdProducto)
        {
            return context.Ofertas.Where(x => x.IdProducto == IdProducto).Count() >= 3;  
        }

    }
}