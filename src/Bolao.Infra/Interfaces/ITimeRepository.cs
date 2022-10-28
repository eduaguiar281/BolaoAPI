using Bolao.Infra.Models.Times;

namespace Bolao.Infra.Interfaces
{
    public interface ITimeRepository
    {
        Task DeleteAsync(Time time);
        Task InsertAsync(Time time);
        Task UpdateAsync(Time time);
        Task<Time> ObterPorIdAsync(int id);
        Task<IEnumerable<Time>> ObterTodosAsync();
        Task<IEnumerable<Time>>ObterPorNome(string nome);
    }
}
