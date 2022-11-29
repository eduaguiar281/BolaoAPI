using Bolao.Application.Interfaces;
using Bolao.Application.ViewModels.Partidas;
using Bolao.Application.ViewModels.Partidas.Eventos;
using Bolao.Application.ViewModels.Partidas.Filtros;
using Bolao.Core.Validations;
using Bolao.Infra.Interfaces;
using Bolao.Infra.Models.Partidas;
using Bolao.Infra.Models.Partidas.Enums;
using Bolao.Infra.Models.Times;
using CSharpFunctionalExtensions;

namespace Bolao.Application.Services
{
    public class PartidaService : IPartidaService
    {
        private readonly IPartidaRepository _partidaRepository;
        private readonly ITimesService _timeService;
        public PartidaService(IPartidaRepository partidaRepository, ITimesService timeService)
        {
            _partidaRepository = partidaRepository;
            _timeService = timeService;
        }

        public Task<Result<Partida>> AnularGolAsync(AnularGolViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        private async Task<Result<Partida>> ObterPartidaPorId(int id)
        {
            Partida partida = await _partidaRepository.ObterPorIdAsync(id);
            return Result.FailureIf(partida is null, partida, "Partida não foi localizada");
        }

        public async Task<Result<Partida>> CancelarAsync(CancelarPartidaViewModel viewModel)
        {
            Result<Partida> partidaResult = await ObterPartidaPorId(viewModel.PartidaId);
            if (partidaResult.IsFailure)
                return partidaResult;
            Result resultValidation = Result.Combine(viewModel.ValidateToResult(),
                Result.FailureIf(partidaResult.Value.DataCancelamento.HasValue, "Partida já foi cancelada"),
                Result.FailureIf(partidaResult.Value.Etapa == Etapa.Finalizada, "Partida já foi finalizada"),
                Result.FailureIf(partidaResult.Value.Etapa != Etapa.NaoIniciada, "Partida já está em andamento!"));
            if (resultValidation.IsFailure)
                return resultValidation.ConvertFailure<Partida>();
            partidaResult.Value.Ativo = false;
            partidaResult.Value.DataCancelamento = DateTime.UtcNow;
            partidaResult.Value.Historicos.Add(new HistoricoPartida
            {
                Evento = Evento.Cancelamento,
                Observacoes = viewModel.Observacoes
            });
            await _partidaRepository.UpdateAsync(partidaResult.Value);
            return partidaResult.Value;
        }

        public Task<Result<Partida>> FinalizarAsync(FimPartidaViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> FinalizarPrimeiroTempoAsync(FimPrimeiroTempoViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> FinalizarPrimeiroTempoProrrogacaoAsync(FimPrimeiroTempoProrrogacaoViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> FinalizarSegundoTempoAsync(FimSegundoTempoViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> FinalizarSegundoTempoProrrogacaoAsync(FimSegundoTempoProrrogacaoViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Partida>> IncluirAsync(CadastrarPartidaViewModel viewModel)
        {
            Result<Time> anfitriaoResult = await _timeService.ObterPorIdAsync(viewModel.AnfitriaoId);
            Result<Time> visitanteResult = await _timeService.ObterPorIdAsync(viewModel.VisitanteId);
            Result validationResult = Result.Combine(viewModel.ValidateToResult(), anfitriaoResult, visitanteResult);

            if (validationResult.IsFailure)
                return validationResult.ConvertFailure<Partida>();

            Partida partida = viewModel.ConvertToModel(anfitriaoResult.Value, visitanteResult.Value);
            await _partidaRepository.InsertAsync(partida);
            return Result.Success(partida);
        }

        public Task<Result<Partida>> IniciarAsync(InicioPartidaViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> IniciarPrimeiroTempoProrrogacaoAsync(InicioPrimeiroTempoProrrogacaoViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> IniciarSegundoTempoAsync(InicioSegundoTempoViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> IniciarSegundoTempoProrrogacaoAsync(InicioSegundoTempoProrrogacaoViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Partida>>> ListarAsync(FiltroListarPartidasViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Partida>> ObterPorIdAsync(int id)
        {
            Partida partida = await _partidaRepository.ObterPorIdAsync(id);
            return Result.FailureIf(partida is null, partida, $"Partida id={id} não foi localizada!");
        }
        public Task<Result<Partida>> ModificarAgendamentoAsync(AgendamentoPartidaViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> RegistrarCartaoAmareloAsync(RegistrarCartaoAmareloViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> RegistrarCartaoVermelhoAsync(RegistrarCartaoVermelhoViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> RegistrarDisputaDePenaltisAsync(InicioDisputaPenaltisViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> RegistrarFaltaAsync(RegistarFaltaViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> RegistrarGolAsync(RegistroGolViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
