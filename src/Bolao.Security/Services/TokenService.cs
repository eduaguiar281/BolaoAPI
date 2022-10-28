using Bolao.Security.Interface;
using Bolao.Security.Models;
using Bolao.Security.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Bolao.Security.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenDto GerarToken(Usuario usuario)
        {
            var tokenConfig = _configuration.GetSection(nameof(TokenConfiguration)).Get<TokenConfiguration>();
            DateTime expireIn = DateTime.UtcNow.AddMinutes(tokenConfig.DurationMinutes);
            string jwt = CriarJWT(usuario, expireIn, tokenConfig);

            usuario.RefreshToken = GenerateRefreshToken();
            usuario.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(tokenConfig.DurationMinutes);
            return new TokenDto
            {
                AccessToken = jwt,
                RefreshToken = usuario.RefreshToken,
                ExpiresIn = expireIn.Ticks,
                RefreshTokenExpiresIn = usuario.RefreshTokenExpiryTime.Value.Ticks,
                Roles = usuario.Roles
            };

        }

        public ClaimsPrincipal ObterClaimsDoJwt(string jwt)
        {
            var tokenConfig = _configuration.GetSection(nameof(TokenConfiguration)).Get<TokenConfiguration>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(jwt, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token Inválido");
            return principal;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private static string CriarJWT(Usuario usuario, DateTime expireIn, TokenConfiguration tokenConfig)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = ObterClaims(usuario),
                Expires = expireIn,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfig.Key)), SecurityAlgorithms.HmacSha256Signature)

            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private static ClaimsIdentity ObterClaims(Usuario user)
        {
            return new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Name, user.NomeUsuario),
                    new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new (ClaimTypes.Email, user.Email),
                    new ("NomeCompleto" , user.NomeCompleto),
                    new (ClaimTypes.Role, user.Roles)
                });
        }

    }
}
