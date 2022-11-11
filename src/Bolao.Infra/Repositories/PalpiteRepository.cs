using Bolao.Infra.Database;
using Bolao.Infra.Interfaces;
using Bolao.Infra.Models.Partidas;
using Microsoft.EntityFrameworkCore;

namespace Bolao.Infra.Repositories
{
    public class PalpiteRepository : IPalpiteRepository
    {
        private readonly BolaoDataContext _context;
        public PalpiteRepository(BolaoDataContext context) => _context = context;

        public async Task DeleteAsync(Palpite palpite)
        {
            if (palpite is null)
                throw new ArgumentNullException(nameof(palpite));
            _context.Palpites.Remove(palpite);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(Palpite palpite)
        {
            if (palpite is null)
                throw new ArgumentNullException(nameof(palpite));
            _context.Palpites.Add(palpite);
            await _context.SaveChangesAsync();
        }

        public async Task<Palpite> ObterPorIdAsync(int id)
        {
            return await _context.Palpites.FindAsync(id);
        }

        public async Task<IEnumerable<Palpite>> ObterPorPartidaIdAsync(int partidaId)
        {
            return await _context.Palpites
                .Include(i => i.Partida)
                .Where(p => p.Partida.Id == partidaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Palpite>> ObterPorUsuarioAsync(string nomeUsuario)
        {
            return await _context.Palpites
                .Include(i => i.Partida)
                .Where(p => p.NomeUsuario == nomeUsuario)
                .ToListAsync();
        }

        public async Task UpdateAsync(Palpite palpite)
        {
            if (palpite is null)
                throw new ArgumentNullException(nameof(palpite));
            _context.Palpites.Update(palpite);
            await _context.SaveChangesAsync();
        }
    }
}
