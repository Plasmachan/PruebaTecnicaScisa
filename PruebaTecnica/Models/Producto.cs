namespace PruebaTecnica.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Producto")]
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            Devoluciones = new HashSet<Devolucione>();
            Ofertas = new HashSet<Oferta>();
        }

        [Key]
        public int IdProducto { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoProducto { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreProducto { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaIngreso { get; set; }

        public decimal ValorCalculado { get; set; }

        //[Column(TypeName = "timestamp")]

        public DateTime TiempoDevolucion { get; set; }

        public bool Bloqueado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Devolucione> Devoluciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oferta> Ofertas { get; set; }


    }
}
