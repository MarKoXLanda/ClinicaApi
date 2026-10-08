using Microsoft.EntityFrameworkCore;
using ClinicaApi.Models;
namespace ClinicaApi.Data
{
    public class ClinicaDbContext : DbContext
    {
        public ClinicaDbContext(DbContextOptions<ClinicaDbContext> options) : base(options) { }

        // Estas propiedades son las que se convertirán en tablas
        public DbSet<CentroCosto> CentrosCosto { get; set; }
        public DbSet<InsumoMedico> Insumos { get; set; }
        public DbSet<TransaccionInventario> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Regla para Db2: Configuración de precisión para el dinero
            modelBuilder.Entity<InsumoMedico>()
                .Property(i => i.PrecioUnitario)
                .HasColumnType("DECIMAL(18,2)");

            // Regla para Db2: Asegurar que el SKU sea único y no haya duplicados
            modelBuilder.Entity<InsumoMedico>()
                .HasIndex(i => i.SKU)
                .IsUnique();
        }
    }
}

