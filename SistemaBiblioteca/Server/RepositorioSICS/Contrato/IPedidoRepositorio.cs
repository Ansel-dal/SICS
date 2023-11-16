using SICS.Server.Models;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Contrato
{
    public interface IPedidoRepositorio
    {
        Task<List<Pedido>> Lista();
        Task<Pedido> Obtener(Expression<Func<Pedido, bool>> filtro = null);
        Task<Pedido> Crear(Pedido entidad);
        Task<bool> Editar(Pedido entidad);
        Task<bool> Eliminar(Pedido entidad);
        Task<IQueryable<Pedido>> Consultar(Expression<Func<Pedido, bool>> filtro = null);
    }
}
