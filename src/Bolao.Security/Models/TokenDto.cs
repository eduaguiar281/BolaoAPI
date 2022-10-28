using System.ComponentModel.DataAnnotations;

namespace Bolao.Security.Models
{
    public class TokenDto
    {
        [Required(ErrorMessage = "Access Token é obrigatório!")]
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public string TokenType { get; set; } = "Bearer";
        [Required(ErrorMessage = "Refresh Token é obrigatório!")]
        public string RefreshToken { get; set; }
        public long RefreshTokenExpiresIn { get; set; }
        public string Roles { get; set; }
    }
}
