using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace PruebaTecnica.Models.ViewModels
{
    public class OfertaGanadoraViewModel
    {
        public IEnumerable<Oferta> Ofertas { get; set; }
        public IEnumerable<Claim> claims { get; set; }
    }
}