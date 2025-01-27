namespace PruebaTecnica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

       
        [StringLength(50)]
        public string Contraseña { get; set; }

  
    
        [StringLength(50)]
        public string NombreUsuario { get; set; }


        [StringLength(50)]
        public string TipoUsuario { get; set; }
    }
}
