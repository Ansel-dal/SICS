using SICS.Client.Pages.Administracion;
using SICS.Server.Models;
using System.Linq.Expressions;

namespace SICS.Server.RepositorioSICS.Contrato
{
    public interface IIdentificadoresRepositorio
    {
        //la tabla se llama NumeroCorrelativo
        Task<List<NumeroCorrelativo>> Lista();
        Task<NumeroCorrelativo> Obtener(Expression<Func<NumeroCorrelativo, bool>> filtro = null);
        Task<bool> Eliminar(NumeroCorrelativo entidad);
        Task<NumeroCorrelativo> Crear(NumeroCorrelativo entidad);
        Task<bool> Editar(NumeroCorrelativo entidad);


    }
}
