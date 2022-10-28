using Bolao.Security.Database;
using Bolao.Security.Interface;
using Bolao.Security.Models;
using Microsoft.EntityFrameworkCore;

namespace Bolao.Security.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioDataContext _context;
        public UsuarioRepository(UsuarioDataContext context)
            => _context = context;

        public async Task<Usuario> ObterUsuario(string NomeUsuario, string Password)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(user => user.NomeUsuario == NomeUsuario &&
                                                                                 user.Password == Password);
        }
        public async Task<Usuario> ObterUsuario(string NomeUsuario)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(user => user.NomeUsuario == NomeUsuario);
        }
        public async Task<IEnumerable<Usuario>> ObterTodosUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }
        public async Task InsertAsync(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUsuario(Usuario usuario)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUsuario(Usuario usuario)
        {
            _context.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
