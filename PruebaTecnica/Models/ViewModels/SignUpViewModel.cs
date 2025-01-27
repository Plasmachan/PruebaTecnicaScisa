using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTecnica.Models.ViewModels
{
    public class SignUpViewModel
    {
        public string Nombre { get; set; } 
        
        public string TipoUsuario { get; set; }

        public string Contraseña { get; set; } 

        public string RepetirContraseña { get; set; } 
    }
}