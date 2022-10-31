using Bolao.Application.Interfaces;
using Bolao.Infra.Interfaces;
using Bolao.Infra.Models.Times;
using CSharpFunctionalExtensions;

namespace Bolao.Application.Services
{
    public class TimeService : ITimesService
    {
        private readonly ITimeRepository _timeRepository;
        public TimeService(ITimeRepository timeRepository)
        {
            _timeRepository = timeRepository;
        }

        public async Task<Result<Time>> AlterarAsync(Time time)
        {
            if (await TimeJaExiste(time.Nome, time.Id))
                return Result.Failure<Time>("Já existe um time com o nome informado!");
            await _timeRepository.UpdateAsync(time);
            return Result.Success(time);
        }

        public async Task<Result> ExcluirAsync(int id)
        {
            Time time = await _timeRepository.ObterPorIdAsync(id);
            if (time is null)
                return Result.Failure($"Time id={id} não foi localizado!");
            await _timeRepository.DeleteAsync(time);
            return Result.Success();
        }

        public async Task<Result<Time>> IncluirAsync(Time time)
        {
            if (await TimeJaExiste(time.Nome))
                return Result.Failure<Time>("Já existe um time com o nome informado!");
            await _timeRepository.InsertAsync(time);
            return Result.Success(time);
        }

        public async Task<Result<IEnumerable<Time>>> ListarTodosAsync()
        {
            return Result.Success(await _timeRepository.ObterTodosAsync());
        }

        public async Task<Result<Time>> ObterPorIdAsync(int id)
        {
            Time time = await _timeRepository.ObterPorIdAsync(id);
            return Result.FailureIf(time is null, time, $"Time id={id} não foi localizado!");
        }

        private async Task<bool> TimeJaExiste(string nome)
        {
            return (await _timeRepository.ObterPorNomeAsync(nome)).Any();
        }
        private async Task<bool> TimeJaExiste(string nome, int id)
        {
            return (await _timeRepository.ObterPorNomeAsync(nome)).Any(time => time.Id != id);
        }
    }
}
