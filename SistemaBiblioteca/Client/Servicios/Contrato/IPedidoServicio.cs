using SICS.Shared;

namespace SICS.Client.Servicios.Contrato
{
    public interface IPedidoServicio
    {
        Task<ResponseDTO<List<PedidoDTO>>> Lista();
        Task<ResponseDTO<List<PedidoDTO>>> Buscar(string estadoPedido, string codigoConsumidor);
        Task<ResponseDTO<PedidoDTO>> Obtener(int idPedido);
        Task<ResponseDTO<PedidoDTO>> Crear(PedidoDTO entidad);
        Task<bool> Editar(PedidoDTO entidad);
        Task<bool> Eliminar(int id);

    }
}
