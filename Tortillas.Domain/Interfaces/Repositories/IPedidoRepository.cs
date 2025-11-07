using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tortillas.Domain.Entities;


namespace Tortillas.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task<Pedido> AddAsync(Pedido pedido);
        Task<Pedido?> GetByIdAsync(int id);
        Task<List<Pedido>> GetAllAsync();
        Task UpdateAsync(Pedido pedido);
        Task DeleteAsync(int id);
        Task<IEnumerable<PedidoDetalleEmpresa>> GetPedidosByEmpresaAsync(int empresaId);
        Task<int> UpdateEstatusDetalleByPedidoIdAsync(int id, string nuevoEstatus);
        Task<int> UpdateFirmaByPedidoIdAsync(int id, string firmaBase64);
    }
}
