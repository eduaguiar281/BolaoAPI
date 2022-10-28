using Bolao.Infra.Models.Times;
using CSharpFunctionalExtensions;

namespace Bolao.Application.Interfaces
{
    public interface ITimesService
    {
        Task<Result<Time>> IncluirAsync(Time time);
        Task<Result<Time>> AlterarAsync(Time time);
        Task<Result> ExcluirAsync(int id);
        Task<Result<IEnumerable<Time>>> ListarTodosAsync();
        Task<Result<Time>> ObterPorIdAsync(int id);
    }
}
