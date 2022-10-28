using Bolao.Security.Models;
using System.Security.Claims;

namespace Bolao.Security.Interface
{
    public interface ITokenService
    {
        TokenDto GerarToken(Usuario usuario);

        ClaimsPrincipal ObterClaimsDoJwt(string jwt);
    }
}
