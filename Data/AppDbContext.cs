using Microsoft.EntityFrameworkCore;

namespace SistemaBoletos.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Cooperativa> Cooperativas { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar cooperativas predefinidas
            modelBuilder.Entity<Cooperativa>().HasData(
                new Cooperativa
                {
                    Id = 1,
                    Nombre = "Imbaburapac",
                    CapacidadMaxima = 45,
                    BoletosVendidos = 0
                },
                new Cooperativa
                {
                    Id = 2,
                    Nombre = "Lagos",
                    CapacidadMaxima = int.MaxValue, // Capacidad ilimitada para simplificar
                    BoletosVendidos = 0
                }
            );
        }
    }
}