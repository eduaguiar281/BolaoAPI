using Bolao.Core.Models;

namespace Bolao.Security.Models
{
    public class Usuario : Entidade<int>
    {
        public string NomeUsuario { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string Roles { get; set; }
    }
}
