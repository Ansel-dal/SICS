using SICS.Shared;

namespace SICS.Client.Servicios.Contrato
{
    public interface IProductoServicio
    {
        Task<ResponseDTO<List<ProductoDTO>>> Lista();
        Task<bool> Eliminar(int id);
        Task<ResponseDTO<ProductoDTO>> Crear(ProductoDTO entidad);
        Task<bool> Editar(ProductoDTO entidad);
        Task<ResponseDTO<List<ProductoDTO>>> Filtrar(string categoriaItem);

    }
}
