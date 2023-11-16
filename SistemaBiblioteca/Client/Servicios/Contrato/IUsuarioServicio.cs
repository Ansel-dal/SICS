using SICS.Shared;

namespace SICS.Client.Servicios.Contrato
{
    public interface IUsuarioServicio
    {
        Task<ResponseDTO<List<UsuarioDTO>>> Lista();
        Task<ResponseDTO<UsuarioDTO>> IniciarSesion(string correo, string clave);
        Task<ResponseDTO<UsuarioDTO>> Crear(UsuarioDTO entidad);
        Task<bool> Editar(UsuarioDTO entidad);
        Task<bool> Eliminar(int id);
    }
}
