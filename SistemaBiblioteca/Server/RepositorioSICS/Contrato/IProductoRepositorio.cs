using SICS.Server.Models;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Contrato
{
    public interface IProductoRepositorio
    {
        Task<List<Producto>> Lista();
        Task<Producto> Obtener(Expression<Func<Producto, bool>> filtro = null);
        Task<bool> Eliminar(Producto entidad);
        Task<Producto> Crear(Producto entidad);
        Task<bool> Editar(Producto entidad);
        Task<IQueryable<Producto>> Consultar(Expression<Func<Producto, bool>> filtro = null);

    }
}
