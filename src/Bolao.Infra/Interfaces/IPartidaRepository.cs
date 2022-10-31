using Bolao.Infra.Models.Partidas;

namespace Bolao.Infra.Interfaces
{
    public interface IPartidaRepository
    {
        Task DeleteAsync(Partida partida);
        Task InsertAsync(Partida partida);
        Task UpdateAsync(Partida partida);
        Task<Partida> ObterPorIdAsync(int id);
        Task<IEnumerable<Partida>> ObterTodosAsync();
        Task<IEnumerable<Partida>> ObterPorDataAsync(DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<Partida>> ObterAbertasAsync();
    }
}
