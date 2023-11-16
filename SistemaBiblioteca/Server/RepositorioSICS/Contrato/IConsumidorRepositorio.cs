using SICS.Server.Models;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Contrato
{
    public interface IConsumidorRepositorio
    {
        Task<List<Consumidor>> Lista();
        Task<Consumidor> Obtener(Expression<Func<Consumidor, bool>> filtro = null);
        Task<Consumidor> Crear(Consumidor entidad);
        Task<bool> Editar(Consumidor entidad);
        Task<bool> Eliminar(Consumidor entidad);
        Task<IQueryable<Consumidor>> Consultar(Expression<Func<Consumidor, bool>> filtro = null);
    }
}
