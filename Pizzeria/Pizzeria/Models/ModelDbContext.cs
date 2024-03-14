using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Pizzeria.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
        }

        public virtual DbSet<Bibita> Bibita { get; set; }
        public virtual DbSet<Ordine> Ordine { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bibita>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Bibita>()
                .HasMany(e => e.Ordine)
                .WithOptional(e => e.Bibita)
                .HasForeignKey(e => e.FK_IdBibita);

            modelBuilder.Entity<Ordine>()
                .Property(e => e.Totale)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Pizza>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pizza>()
                .HasMany(e => e.Ordine)
                .WithRequired(e => e.Pizza)
                .HasForeignKey(e => e.FK_IdPizza)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Ordine)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.FK_IdUtente)
                .WillCascadeOnDelete(false);
        }
    }
}
