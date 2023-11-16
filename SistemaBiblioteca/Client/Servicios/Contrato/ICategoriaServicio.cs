using SICS.Shared;

namespace SICS.Client.Servicios.Contrato
{
    public interface ICategoriaServicio
    {
        Task<ResponseDTO<List<CategoriaDTO>>> Lista();
        Task<bool> Eliminar(int id);
        Task<ResponseDTO<CategoriaDTO>> Crear(CategoriaDTO entidad);
        Task<bool> Editar(CategoriaDTO entidad);
        Task<ResponseDTO<CategoriaDTO>> Filtrar(string descripcion);

    }
}
