using Bolao.Security.Models;

namespace Bolao.Security.Interface
{
    public interface IUsuarioRepository
    {
        Task DeleteUsuario(Usuario usuario);
        Task InsertAsync(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
        Task<Usuario> ObterUsuario(string nomeUsuario, string password);
        Task<Usuario> ObterUsuario(string nomeUsuario);
        Task<IEnumerable<Usuario>> ObterTodosUsuarios();
    }
}
