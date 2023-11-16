using SICS.Shared;

namespace SICS.Client.Servicios.Contrato
{
    public interface IConsumidorServicio
    {
        Task<ResponseDTO<List<ConsumidorDTO>>> Lista();
        Task<ResponseDTO<ConsumidorDTO>> Obtener(int idConsumidor);
        Task<ResponseDTO<ConsumidorDTO>> Crear(ConsumidorDTO entidad);
        Task<bool> Editar(ConsumidorDTO entidad);
        Task<bool> Eliminar(int id);
    }
}
