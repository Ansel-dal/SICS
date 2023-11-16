using System.ComponentModel.DataAnnotations;

namespace SICS.Client.Utilidad
{
    public class UsuarioLogin
    {
        [Required(ErrorMessage = "El correo es requerida.")]
        public string correo { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string clave { get; set; }
    }
}
