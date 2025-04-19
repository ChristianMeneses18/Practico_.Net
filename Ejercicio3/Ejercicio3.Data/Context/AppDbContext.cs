using Ejercicio3.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio3.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Categoria>().ToTable("Categoria");
        }
    }
}
