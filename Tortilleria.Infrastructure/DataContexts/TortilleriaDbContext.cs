using Microsoft.EntityFrameworkCore;
using Tortillas.Domain.Entities;

namespace Tortilleria.Infrastructure.DataContexts
{
    public class TortilleriaDbContext : DbContext
    {
        public DbSet<Usuario> Usuario => Set<Usuario>();
        public DbSet<Vehiculo> Vehiculo => Set<Vehiculo>();
        public DbSet<Empresa> Empresa => Set<Empresa>();
        public DbSet<Sucursal> Sucursal => Set<Sucursal>();
        public DbSet<Rol> Rol => Set<Rol>();
        public DbSet<Direccion> Direccion => Set<Direccion>();
        public DbSet<Pedido> Pedido => Set<Pedido>();
        public DbSet<DetallePedido> DetallePedido => Set<DetallePedido>();
        public DbSet<Pago> Pago => Set<Pago>();

        public TortilleriaDbContext(DbContextOptions<TortilleriaDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Empresa - Sucursal (1:N)
            modelBuilder.Entity<Sucursal>()
                .HasOne(s => s.Empresa)
                .WithMany(e => e.Sucursales)
                .HasForeignKey(s => s.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Pedido -> DetallePedido
            modelBuilder.Entity<DetallePedido>()
                .HasOne<Pedido>()
                .WithMany(p => p.Detalles)
                .HasForeignKey(d => d.FkPedido)
                .OnDelete(DeleteBehavior.Cascade);

            // Pedido -> Pago (1:1)
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Pago)
                .WithOne()
                .HasForeignKey<Pago>(pago => pago.FkPedido)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
