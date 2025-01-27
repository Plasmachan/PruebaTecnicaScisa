using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PruebaTecnica.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Devolucione> Devoluciones { get; set; }
        public virtual DbSet<Oferta> Ofertas { get; set; }
        public virtual DbSet<Producto> Productoes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Devolucione>()
                .Property(e => e.ComentarioDevolucion)
                .IsUnicode(false);

            modelBuilder.Entity<Oferta>()
                .Property(e => e.Ofertante)
                .IsUnicode(false);

            modelBuilder.Entity<Oferta>()
                .Property(e => e.NumeroOfertante)
                .IsUnicode(false);

            modelBuilder.Entity<Oferta>()
                .Property(e => e.MontoOferta)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Producto>()
                .Property(e => e.TipoProducto)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.NombreProducto)
                .IsUnicode(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.ValorCalculado)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.Devoluciones)
                .WithRequired(e => e.Producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.Ofertas)
                .WithRequired(e => e.Producto)
                .WillCascadeOnDelete(false);

      

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Contraseña)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.NombreUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.TipoUsuario)
                .IsUnicode(false);
        }
    }
}
