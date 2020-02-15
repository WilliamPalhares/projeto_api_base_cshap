using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Cadastro.Models
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<ClienteModel> Cliente { get; set; }
        public virtual DbSet<PaisModel> Pais { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClienteModel>().HasKey(t => t.Id);
            modelBuilder.Entity<ClienteModel>().HasIndex(t => t.Nome).IsUnique();
            modelBuilder.Entity<ClienteModel>().Property(t => t.Id).HasColumnName("Id").IsRequired();

            modelBuilder.Entity<PaisModel>().HasKey(t => t.Id);
            modelBuilder.Entity<PaisModel>().Property(t => t.Id).HasColumnName("Id").IsRequired();
        }
    }
}
