using SICS.Shared;

namespace SICS.Client.Servicios.Contrato
{
    public interface IEntregaServicio
    {
        Task<ResponseDTO<List<EntregaDTO>>> Lista();
        Task<ResponseDTO<List<EntregaDTO>>> Buscar(string codigoConsumidor);
        Task<ResponseDTO<EntregaDTO>> Obtener(int idEntrega);
        Task<ResponseDTO<EntregaDTO>> Crear(EntregaDTO entidad);
        Task<bool> Editar(EntregaDTO entidad);
        Task<bool> Eliminar(int id);

    }
}
