using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;
using Tortillas.Domain.Interfaces.Repositories;
using Tortillas.Infrastructure.DataContexts;

namespace Tortillas.Infrastructure.Persistence
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly TortillasDbContext _context;

        public PedidoRepository(TortillasDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> AddAsync(Pedido pedido)
        {
            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _context.Pedido
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pedido>> GetAllAsync()
        {
            return await _context.Pedido.ToListAsync();
        }

        public async Task UpdateAsync(Pedido pedido)
        {
            _context.Pedido.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<PedidoDetalleEmpresa>> GetPedidosByEmpresaAsync(int idEmpresa)
        {
            var result = await (
                from e in _context.Empresa
                join p in _context.Pedido on e.Id equals p.FkEmpresa
                join dp in _context.DetallePedido on p.Id equals dp.FkPedido
                join dps in _context.DetallePedidoSucursal on dp.Id equals dps.FkDetallePedido
                join s in _context.Sucursal on dps.FkSucursal equals s.Id
                join d in _context.Direccion on s.FkDireccion equals d.Id
                where e.Id == idEmpresa && dp.EstatusDetalle == "Pendiente"

                select new PedidoDetalleEmpresa
                {
                    IdPedido = (int)dp.FkPedido,
                    Empresa = e.NombreEmpresa,
                    NombreEncargado = s.NombreEncargado,
                    Sucursal = s.NombreSucursal,
                    EstatusGeneral = p.EstatusGeneral,
                    EstatusDetalle = dp.EstatusDetalle,
                    FechaHora = dp.FechaHora ?? DateTime.MinValue,
                    Cantidad = (int?)dp.Cantidad,
                    Total = p.Total,
                    Producto = dp.ProductoNombre,

                    Calle = d.Calle,
                    Numero = d.Numero,
                    Colonia = d.Colonia,
                    Ciudad = d.Ciudad,
                    Estado = d.Estado,
                    CodigoPostal = d.CP,
                }
            ).ToListAsync();

            return result;
        }

        public async Task<int> UpdateEstatusDetalleByPedidoIdAsync(int idPedido, string nuevoEstatus)
        {
            var detalles = await _context.DetallePedido
                .Where(dp => dp.FkPedido == idPedido)
                .ToListAsync();

            if (!detalles.Any()) return 0;

            foreach (var detalle in detalles)
            {
                detalle.EstatusDetalle = nuevoEstatus;
                detalle.FechaUltimaModificacion = DateTime.Now;
            }

            return await _context.SaveChangesAsync(); 
        }

        public async Task<int> UpdateFirmaByPedidoIdAsync(int idPedido, string firmaBase64)
        {
            var detalles = await _context.DetallePedido
                .Where(dp => dp.FkPedido == idPedido)
                .ToListAsync();

            if (!detalles.Any()) return 0;

            foreach (var detalle in detalles)
            {
                detalle.Firma = firmaBase64;
                detalle.FechaUltimaModificacion = DateTime.Now;
            }

            return await _context.SaveChangesAsync();
        }
    }
   
}



