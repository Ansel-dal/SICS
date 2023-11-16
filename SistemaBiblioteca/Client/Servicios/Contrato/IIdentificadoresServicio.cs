
using SICS.Shared;

namespace SICS.Client.Servicios.Contrato
{
    public interface IIdentificadoresServicio
    {
        Task<ResponseDTO<List<IdentificadoresDTO>>> Lista();
        Task<bool> Eliminar(int id);
        Task<ResponseDTO<IdentificadoresDTO>> Crear(IdentificadoresDTO entidad);
        Task<bool> Editar(IdentificadoresDTO entidad);

    }
}
