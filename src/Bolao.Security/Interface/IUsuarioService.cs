using Bolao.Security.Models;
using CSharpFunctionalExtensions;

namespace Bolao.Security.Interface
{
    public interface IUsuarioService
    {
        Task<Result<TokenDto>> LoginAsync(string username, string password);
        Task<Result<TokenDto>> RefreshTokenAsync(TokenDto tokenDto);
        Task<Result> RevokeAsync(string userName);
        Task RevokeAllAsync();
    }
}
