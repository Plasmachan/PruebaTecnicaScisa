using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTecnica.Models.ViewModels
{
    public class ProductosOfertadosViewModel
    {


        public ProductosOfertadosViewModel(Producto p)
        {
            this.TipoProducto = p.TipoProducto;
            this.IdProducto = p.IdProducto;
            this.NombreProducto = p.NombreProducto;
            this.FechaIngreso = p.FechaIngreso;
            this.ValorCalculado = p.ValorCalculado;
            this.TiempoDevolucion = p.TiempoDevolucion;
            this.Bloqueado = p.Bloqueado;
        }


        public int IdProducto { get; set; }
        public string TipoProducto { get; set; }
        public string NombreProducto { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal ValorCalculado { get; set; }
        public DateTime TiempoDevolucion { get; set; }
        public bool Bloqueado { get; set; }


        public bool TieneDevolcion { get; set; }
        public bool Tiene3Ofertas { get; set; }
        public Oferta OfertaMayor { get; set; }
        
        
    }
}