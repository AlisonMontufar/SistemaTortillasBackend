using Microsoft.EntityFrameworkCore;
using Tortillas.Domain.Entities;

namespace Tortilleria.Infrastructure.DataContexts
{
    public class TortilleriaDbContext : DbContext
    {
        public TortilleriaDbContext(DbContextOptions<TortilleriaDbContext> options)
          : base(options) { }
        public DbSet<Sucursal> Sucursal => Set<Sucursal>();
        public DbSet<Usuario> Usuario => Set<Usuario>();
        public DbSet<Vehiculo> Vehiculo => Set<Vehiculo>();
        public DbSet<Rol> Rol => Set<Rol>();
        public DbSet<Pedido> Pedido => Set<Pedido>();
        public DbSet<DetallePedido> DetallePedido => Set<DetallePedido>();
        public DbSet<Pago> Pago => Set<Pago>();
    }
}
