using Microsoft.EntityFrameworkCore;
using Tortillas.Domain.Entities;

namespace Tortillas.Infrastructure.DataContexts
{
    public class TortillasDbContext : DbContext
    {
        public TortillasDbContext(DbContextOptions<TortillasDbContext> options)
          : base(options) { }
        public DbSet<Sucursal> Sucursal => Set<Sucursal>();
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<Usuario> Usuario => Set<Usuario>();
        public DbSet<Vehiculo> Vehiculo => Set<Vehiculo>();
        public DbSet<Rol> Rol => Set<Rol>();
        public DbSet<Pedido> Pedido => Set<Pedido>();

        public DbSet<DetallePedido> DetallePedido => Set<DetallePedido>();
        public DbSet<Pago> Pago => Set<Pago>();
        public DbSet<DetallePedidoSucursal> DetallePedidoSucursal => Set<DetallePedidoSucursal>();
    
    }
}
