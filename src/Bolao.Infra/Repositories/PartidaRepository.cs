using Bolao.Infra.Database;
using Bolao.Infra.Interfaces;
using Bolao.Infra.Models.Partidas;
using Bolao.Infra.Models.Partidas.Enums;
using Microsoft.EntityFrameworkCore;

namespace Bolao.Infra.Repositories
{
    public class PartidaRepository : IPartidaRepository
    {
        private readonly BolaoDataContext _context;
        public PartidaRepository(BolaoDataContext context) => _context = context;

        public async Task DeleteAsync(Partida partida)
        {
            if (partida is null)
                throw new ArgumentNullException(nameof(partida));
            _context.Partidas.Remove(partida);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync(Partida partida)
        {
            if (partida is null)
                throw new ArgumentNullException(nameof(partida));
            _context.Partidas.Add(partida);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Partida>> ObterAbertasAsync()
        {
            return await _context.Partidas.Where(p => ((p.Etapa != Etapa.Finalizada) && (p.Ativo.HasValue && p.Ativo.Value))).ToListAsync();
        }

        public async Task<IEnumerable<Partida>> ObterPorDataAsync(DateTime? dataInicio, DateTime? dataFim)
        {
            IQueryable<Partida> partidaQueryable = _context.Partidas.AsQueryable();
            if (dataInicio.HasValue)
                partidaQueryable = partidaQueryable.Where(p => p.Data >= dataInicio);
            
            if (dataFim.HasValue)
                partidaQueryable = partidaQueryable.Where(p => p.Data <= dataFim);
            
            return await partidaQueryable.ToListAsync();
        }

        public async Task<Partida> ObterPorIdAsync(int id)
        {
            return await _context.Partidas.Include(i => i.Historicos).AsSplitQuery()
                .Include(i => i.Anfitriao)
                .Include(i => i.Visitante)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Partida>> ObterTodosAsync()
        {
            return await _context.Partidas.ToListAsync();
        }

        public async Task UpdateAsync(Partida partida)
        {
            if (partida is null)
                throw new ArgumentNullException(nameof(partida));
            _context.Partidas.Update(partida);
            await _context.SaveChangesAsync();
        }
    }
}
