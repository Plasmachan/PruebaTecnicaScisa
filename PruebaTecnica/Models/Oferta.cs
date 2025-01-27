namespace PruebaTecnica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Oferta")]
    public partial class Oferta
    {
        [Key]
        public int IdOferta { get; set; }

        public int IdProducto { get; set; }

        [Required]
        [StringLength(50)]
        public string Ofertante { get; set; }

        [Required]
        [StringLength(50)]
        public string NumeroOfertante { get; set; }


        public decimal MontoOferta { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaOferta { get; set; }

        public virtual Producto Producto { get; set; }
    }
}
