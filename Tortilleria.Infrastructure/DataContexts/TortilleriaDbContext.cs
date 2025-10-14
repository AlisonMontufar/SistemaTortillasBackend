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

        public IEnumerable<object> Role { get; internal set; }

        public TortilleriaDbContext(DbContextOptions<TortilleriaDbContext> options) : base(options) { }
    }
}
