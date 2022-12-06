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

        public async Task<Result<Partida>> AnularGolAsync(AnularGolViewModel viewModel)
        {
            Result<Partida> partidaResult = await ObterPartidaPorId(viewModel.PartidaId);
            if (partidaResult.IsFailure)
                return partidaResult;
            
            Result<HistoricoPartida> historicoResult = ObterHistorico(viewModel.HistoricoId, partidaResult.Value);
            if (historicoResult.IsFailure)
                return historicoResult.ConvertFailure<Partida>();
            
            Result validationResult = Result.Combine(viewModel.ValidateToResult(), ValidarPartidaCancelada(partidaResult.Value),
                ValidarPartidaDeveEstarEmAndamento(partidaResult.Value),
                Result.FailureIf(historicoResult.Value.Evento != Evento.Gol, "Histórico de Evento não é de Gol!"));
            if (validationResult.IsFailure)
                return validationResult.ConvertFailure<Partida>();

            if (historicoResult.Value.Time.Id == partidaResult.Value.Anfitriao.Id)
                partidaResult.Value.GolsAnfitriao -= 1;
            else
                partidaResult.Value.GolsVisitante -= 1;

            if (partidaResult.Value.GolsVisitante == partidaResult.Value.GolsAnfitriao)
                partidaResult.Value.Resultado = ResultadoPartida.Empate;
            else if (partidaResult.Value.GolsVisitante > partidaResult.Value.GolsAnfitriao)
                partidaResult.Value.Resultado = ResultadoPartida.VitoriaVisitante;
            else if (partidaResult.Value.GolsVisitante < partidaResult.Value.GolsAnfitriao)
                partidaResult.Value.Resultado = ResultadoPartida.VitoriaAnfitriao;

            partidaResult.Value.Historicos.Add(new HistoricoPartida()
            {
                Evento = Evento.GolAnulado,
                Minuto = viewModel.Minuto,
                Time = historicoResult.Value.Time,
                Observacoes = $@"Gol de {historicoResult.Value.Jogador} do {historicoResult.Value.Time.Nome} foi anulado.
{viewModel.Observacoes}"
            });
            await _partidaRepository.UpdateAsync(partidaResult.Value);
            return partidaResult;
        }

        public async Task<Result<Partida>> CancelarAsync(CancelarPartidaViewModel viewModel)
        {
            Result<Partida> partidaResult = await ObterPartidaPorId(viewModel.PartidaId);
            if (partidaResult.IsFailure)
                return partidaResult;
            Result resultValidation = Result.Combine(viewModel.ValidateToResult(), ValidarPartidaCancelada(partidaResult.Value),
                ValidarPartidaFinalizada(partidaResult.Value), ValidarPartidaInciada(partidaResult.Value));
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

        public async Task<Result<Partida>> FinalizarPrimeiroTempoAsync(FimPrimeiroTempoViewModel viewModel)
        {
            Result<Partida> partidaResult = await ObterPartidaPorId(viewModel.PartidaId);
            if (partidaResult.IsFailure)
                return partidaResult;
            Result resultValidation = Result.Combine(ValidarPartidaCancelada(partidaResult.Value), ValidarPartidaFinalizada(partidaResult.Value),
                ValidarPrimeiroTempoEmAndamento(partidaResult.Value));
            
            if (resultValidation.IsFailure)
                return resultValidation.ConvertFailure<Partida>();
            
            partidaResult.Value.Etapa = Etapa.Intervalo;
            partidaResult.Value.Historicos.Add(new HistoricoPartida
            {
                Evento = Evento.FimEtapa,
                Observacoes = $@"Fim do {_PRIMEIRO_TEMPO}.
{viewModel.Observacoes}"
            });
            await _partidaRepository.UpdateAsync(partidaResult.Value);
            return partidaResult.Value;

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

        public async Task<Result<Partida>> IniciarAsync(InicioPartidaViewModel viewModel)
        {
            Result<Partida> partidaResult = await ObterPartidaPorId(viewModel.PartidaId);
            if (partidaResult.IsFailure)
                return partidaResult;
            Result resultValidation = Result.Combine(Result.FailureIf(partidaResult.Value.Data > DateTime.UtcNow, "Não é permitido iniciar partida antes do horário marcado!"),
                ValidarPartidaCancelada(partidaResult.Value), ValidarPartidaFinalizada(partidaResult.Value), 
                ValidarPartidaInciada(partidaResult.Value));
            if (resultValidation.IsFailure)
                return resultValidation.ConvertFailure<Partida>();
            partidaResult.Value.Etapa = Etapa.PrimeiroTempo;
            partidaResult.Value.Historicos.Add(new HistoricoPartida
            {
                Evento = Evento.InicioEtapa,
                Observacoes = $@"Início {_PRIMEIRO_TEMPO}.
{viewModel.Observacoes}"
            });
            await _partidaRepository.UpdateAsync(partidaResult.Value);
            return partidaResult.Value;
        }

        public Task<Result<Partida>> IniciarPrimeiroTempoProrrogacaoAsync(InicioPrimeiroTempoProrrogacaoViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Partida>> IniciarSegundoTempoAsync(InicioSegundoTempoViewModel viewModel)
        {
            Result<Partida> partidaResult = await ObterPartidaPorId(viewModel.PartidaId);
            if (partidaResult.IsFailure)
                return partidaResult;
            Result resultValidation = Result.Combine(ValidarPartidaCancelada(partidaResult.Value), ValidarPartidaFinalizada(partidaResult.Value),
                ValidarInicioSegundoTempo(partidaResult.Value));
            if (resultValidation.IsFailure)
                return resultValidation.ConvertFailure<Partida>();
            partidaResult.Value.Etapa = Etapa.SegundoTempo;
            partidaResult.Value.Historicos.Add(new HistoricoPartida
            {
                Evento = Evento.InicioEtapa,
                Observacoes = $@"Início {_SEGUNDO_TEMPO}.
{viewModel.Observacoes}"
            });
            await _partidaRepository.UpdateAsync(partidaResult.Value);
            return partidaResult.Value;
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
        public async Task<Result<Partida>> ModificarAgendamentoAsync(AgendamentoPartidaViewModel viewModel)
        {
            Result<Partida> partidaResult = await ObterPartidaPorId(viewModel.PartidaId);
            if (partidaResult.IsFailure)
                return partidaResult;
            Result validationResult = Result.Combine(viewModel.ValidateToResult(), ValidarPartidaCancelada(partidaResult.Value), ValidarPartidaFinalizada(partidaResult.Value),
                ValidarPartidaInciada(partidaResult.Value));
            if (validationResult.IsFailure)
                return validationResult.ConvertFailure<Partida>();

            partidaResult.Value.Local = viewModel.Local;
            partidaResult.Value.Data = viewModel.Data;
            await _partidaRepository.UpdateAsync(partidaResult.Value);

            return partidaResult;
        }

        public async Task<Result<Partida>> RegistrarCartaoAmareloAsync(RegistrarCartaoAmareloViewModel viewModel)
        {
            Result<Partida> partidaResult = await ObterPartidaPorId(viewModel.PartidaId);
            if (partidaResult.IsFailure)
                return partidaResult;
            Result validationResult = Result.Combine(viewModel.ValidateToResult(), ValidarPartidaCancelada(partidaResult.Value),
                ValidarPartidaDeveEstarEmAndamento(partidaResult.Value));

            if (validationResult.IsFailure)
                return validationResult.ConvertFailure<Partida>();
            Time timeEvento = viewModel.EhEventoDoAnfitriao ? partidaResult.Value.Anfitriao : partidaResult.Value.Visitante;

            partidaResult.Value.Historicos.Add(new HistoricoPartida()
            {
                Evento = Evento.CartaoAmarelo,
                Jogador = viewModel.NomeJogador,
                Minuto = viewModel.Minuto,
                Time = timeEvento,
                Observacoes = $@"Cartão amarelo para {viewModel.NomeJogador} do {timeEvento.Nome}
{viewModel.Observacoes}"
            });
            await _partidaRepository.UpdateAsync(partidaResult.Value);
            return partidaResult;
        }

        public async Task<Result<Partida>> RegistrarCartaoVermelhoAsync(RegistrarCartaoVermelhoViewModel viewModel)
        {
            Result<Partida> partidaResult = await ObterPartidaPorId(viewModel.PartidaId);
            if (partidaResult.IsFailure)
                return partidaResult;
            Result validationResult = Result.Combine(viewModel.ValidateToResult(), ValidarPartidaCancelada(partidaResult.Value),
                ValidarPartidaDeveEstarEmAndamento(partidaResult.Value));

            if (validationResult.IsFailure)
                return validationResult.ConvertFailure<Partida>();
            Time timeEvento = viewModel.EhEventoDoAnfitriao ? partidaResult.Value.Anfitriao : partidaResult.Value.Visitante;

            partidaResult.Value.Historicos.Add(new HistoricoPartida()
            {
                Evento = Evento.CartaoVermelho,
                Jogador = viewModel.NomeJogador,
                Minuto = viewModel.Minuto,
                Time = timeEvento,
                Observacoes = $@"Cartão vermelho para {viewModel.NomeJogador} do {timeEvento.Nome}
{viewModel.Observacoes}"
            });
            await _partidaRepository.UpdateAsync(partidaResult.Value);
            return partidaResult;
        }

        public Task<Result<Partida>> RegistrarDisputaDePenaltisAsync(InicioDisputaPenaltisViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Partida>> RegistrarFaltaAsync(RegistarFaltaViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Partida>> RegistrarGolAsync(RegistroGolViewModel viewModel)
        {
            Result<Partida> partidaResult = await ObterPartidaPorId(viewModel.PartidaId);
            if (partidaResult.IsFailure)
                return partidaResult;
            Result validationResult = Result.Combine(viewModel.ValidateToResult(), ValidarPartidaCancelada(partidaResult.Value), 
                ValidarPartidaDeveEstarEmAndamento(partidaResult.Value));
            
            if (validationResult.IsFailure)
                return validationResult.ConvertFailure<Partida>();
            if (viewModel.EhEventoDoAnfitriao)
                partidaResult.Value.GolsAnfitriao += 1;
            else
                partidaResult.Value.GolsVisitante += 1;

            if (partidaResult.Value.GolsVisitante == partidaResult.Value.GolsAnfitriao)
                partidaResult.Value.Resultado = ResultadoPartida.Empate;
            else if (partidaResult.Value.GolsVisitante > partidaResult.Value.GolsAnfitriao)
                partidaResult.Value.Resultado = ResultadoPartida.VitoriaVisitante;
            else if (partidaResult.Value.GolsVisitante < partidaResult.Value.GolsAnfitriao)
                partidaResult.Value.Resultado = ResultadoPartida.VitoriaAnfitriao;

            Time timeEvento = viewModel.EhEventoDoAnfitriao ? partidaResult.Value.Anfitriao : partidaResult.Value.Visitante;
            partidaResult.Value.Historicos.Add(new HistoricoPartida()
            {
                Evento = Evento.Gol,
                Jogador = viewModel.NomeJogador,
                Minuto = viewModel.Minuto,
                Time = timeEvento,
                Observacoes = $@"Gol de {viewModel.NomeJogador} do {timeEvento.Nome}
{viewModel.Observacoes}"
            });
            await _partidaRepository.UpdateAsync(partidaResult.Value);
            return partidaResult;
        }

        #region Métodos Privados

        private async Task<Result<Partida>> ObterPartidaPorId(int id)
        {
            Partida partida = await _partidaRepository.ObterPorIdAsync(id);
            return Result.FailureIf(partida is null, partida, "Partida não foi localizada");
        }

        private static Result<HistoricoPartida> ObterHistorico(int historicoId, Partida partida)
        {
            ArgumentNullException.ThrowIfNull(partida);
            HistoricoPartida historico = partida.Historicos.FirstOrDefault(h => h.Id == historicoId);
            return Result.FailureIf(historico is null, historico, "Histórico de evento não foi localizado!");
        }

        private static Result<Partida> ValidarPartidaCancelada(Partida partida)
        {
            return partida is null ? throw new ArgumentNullException()
                : Result.FailureIf(partida.DataCancelamento.HasValue, partida, "Partida foi cancelada");
        }
        private static Result<Partida> ValidarPartidaFinalizada(Partida partida)
        {
            return partida is null ? throw new ArgumentNullException()
                : Result.FailureIf(partida.Etapa == Etapa.Finalizada, partida, "Partida foi finalizada");
        }
        private static Result<Partida> ValidarPartidaInciada(Partida partida)
        {
            return partida is null ? throw new ArgumentNullException()
                : Result.FailureIf(partida.Etapa != Etapa.NaoIniciada, partida, "Partida já foi iniciada!");
        }
        private static Result<Partida> ValidarPartidaDeveEstarEmAndamento(Partida partida)
        {
            Etapa partidaParada = Etapa.Intervalo | Etapa.NaoIniciada | Etapa.Finalizada;
            return partida is null
                ? throw new ArgumentNullException()
                : Result.FailureIf(partidaParada.HasFlag(partida.Etapa), partida, "Partida deve estar em andamento!");
        }

        private const string _PRIMEIRO_TEMPO = "1º tempo";
        private const string _SEGUNDO_TEMPO = "2º tempo";

        private static Result<Partida> ValidarPrimeiroTempoEmAndamento(Partida partida)
        {
            return partida is null
                ? throw new ArgumentNullException()
                : Result.FailureIf(partida.Etapa != Etapa.PrimeiroTempo, partida, "Partida não está no primeiro tempo!");
        }
        private static Result ValidarInicioSegundoTempo(Partida partida)
        {
            return partida is null
                ? throw new ArgumentNullException()
                : Result.Combine<Partida>(
                  Result.SuccessIf<Partida>(partida.Etapa == Etapa.Intervalo, partida, "Partida não está no intervalo!"),
                  Result.SuccessIf<Partida>((partida.Etapa == Etapa.Intervalo) && 
                    (partida.Historicos.Count(h => h.Evento == Evento.InicioEtapa) == 1), 
                     partida, "Primeiro tempo não foi finalizado!")); 
        }
        #endregion
    }
}
