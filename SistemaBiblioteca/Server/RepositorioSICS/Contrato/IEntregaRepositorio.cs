using SICS.Server.Models;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Contrato
{
    public interface IEntregaRepositorio
    {
        Task<List<Entrega>> Lista();
        Task<Entrega> Obtener(Expression<Func<Entrega, bool>> filtro = null);
        Task<Entrega> Crear(Entrega entidad);
        Task<bool> Editar(Entrega entidad);
        Task<bool> Eliminar(Entrega entidad);
        Task<IQueryable<Entrega>> Consultar(Expression<Func<Entrega, bool>> filtro = null);
    }
}
