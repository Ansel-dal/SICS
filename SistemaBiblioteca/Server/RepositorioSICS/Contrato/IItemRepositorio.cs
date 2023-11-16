using SICS.Server.Models;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Contrato
{
    public interface IItemRepositorio
    {
        Task<List<Item>> Lista();
        Task<Item> Obtener(Expression<Func<Item, bool>> filtro = null);
        Task<Item> Crear(Item entidad);
        Task<bool> Editar(Item entidad);
        Task<bool> Eliminar(Item entidad);
        Task<IQueryable<Item>> Consultar(Expression<Func<Item, bool>> filtro = null);
    }
}
