using Microsoft.EntityFrameworkCore;
using Tortillas.Domain.Entities;

namespace Tortilleria.Infrastructure.DataContexts
{
    public class TortilleriaDbContext : DbContext
    {
        public DbSet<Usuario> Usuario => Set<Usuario>();
        public DbSet<Vehiculo> Vehiculo { get; set; } = null!;
        public DbSet<Empresa> Empresa => Set<Empresa>();
        public DbSet<Rol> Rol => Set<Rol>();
        public DbSet<Direccion> Direccion => Set<Direccion>();
        public DbSet<Pedido> Pedido => Set<Pedido>();
        public DbSet<DetallePedido> DetallePedido => Set<DetallePedido>();
        public DbSet<Pago> Pago => Set<Pago>();
        public IEnumerable<object> Role { get; internal set; }

        public TortilleriaDbContext(DbContextOptions<TortilleriaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Pedido -> DetallePedido
            modelBuilder.Entity<DetallePedido>()
                .HasOne<Pedido>()
                .WithMany(p => p.Detalles)
                .HasForeignKey(d => d.FkPedido)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Pago)
                .WithOne() // sin propiedad de navegación inversa
                .HasForeignKey<Pago>(pago => pago.FkPedido)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
