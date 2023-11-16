using SICS.Shared;

namespace SICS.Client.Servicios.Contrato
{
    public interface IDashBoardServicio
    {
        Task<ResponseDTO<DashBoardDTO>> Resumen();
    }
}
