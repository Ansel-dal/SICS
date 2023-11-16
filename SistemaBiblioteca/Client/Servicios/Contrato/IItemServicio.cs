using SICS.Shared;

namespace SICS.Client.Servicios.Contrato
{
    public interface IItemServicio
    {
        Task<ResponseDTO<List<ItemDTO>>> Lista();
        Task<ResponseDTO<ItemDTO>> Obtener(int idItem);
        Task<ResponseDTO<List<ItemDTO>>> Buscar(string valor);
        Task<ResponseDTO<List<ItemDTO>>> Filtrar(string categoriaItem);
        Task<ResponseDTO<ItemDTO>> Crear(ItemDTO entidad);
        Task<bool> Editar(ItemDTO entidad);
        Task<bool> Eliminar(int id);
    }
}
