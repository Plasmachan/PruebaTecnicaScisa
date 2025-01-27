namespace PruebaTecnica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Devolucione
    {
        [Key]
        public int IdDevolucion { get; set; }

        public int IdProducto { get; set; }

        [Required]
        [StringLength(50)]
        public string ComentarioDevolucion { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaDevolucion { get; set; }

        public bool EstadoDevolucion { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
