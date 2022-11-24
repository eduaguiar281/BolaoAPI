using Bolao.Application.ViewModels.Partidas;
using Bolao.Application.ViewModels.Partidas.Eventos;
using Bolao.Application.ViewModels.Partidas.Filtros;
using Bolao.Infra.Models.Partidas;
using CSharpFunctionalExtensions;

namespace Bolao.Application.Interfaces
{
    public interface IPartidaService
    {
        Task<Result<IEnumerable<Partida>>> ListarAsync(FiltroListarPartidasViewModel viewModel);
        Task<Result<Partida>> IncluirAsync(CadastrarPartidaViewModel viewModel);
        Task<Result<Partida>> ModificarAgendamentoAsync(AgendamentoPartidaViewModel viewModel);
        Task<Result<Partida>> CancelarAsync(CancelarPartidaViewModel viewModel);
        Task<Result<Partida>> RegistrarGolAsync(RegistroGolViewModel viewModel);
        Task<Result<Partida>> AnularGolAsync(AnularGolViewModel viewModel);
        Task<Result<Partida>> RegistrarFaltaAsync(RegistarFaltaViewModel viewModel);
        Task<Result<Partida>> RegistrarCartaoAmareloAsync(RegistrarCartaoAmareloViewModel viewModel);
        Task<Result<Partida>> RegistrarCartaoVermelhoAsync(RegistrarCartaoVermelhoViewModel viewModel);
        Task<Result<Partida>> IniciarAsync(InicioPartidaViewModel viewModel);
        Task<Result<Partida>> FinalizarPrimeiroTempoAsync(FimPrimeiroTempoViewModel viewModel);
        Task<Result<Partida>> IniciarSegundoTempoAsync(InicioSegundoTempoViewModel viewModel);
        Task<Result<Partida>> FinalizarSegundoTempoAsync(FimSegundoTempoViewModel viewModel);
        Task<Result<Partida>> IniciarPrimeiroTempoProrrogacaoAsync(InicioPrimeiroTempoProrrogacaoViewModel viewModel);
        Task<Result<Partida>> FinalizarPrimeiroTempoProrrogacaoAsync(FimPrimeiroTempoProrrogacaoViewModel viewModel);
        Task<Result<Partida>> IniciarSegundoTempoProrrogacaoAsync(InicioSegundoTempoProrrogacaoViewModel viewModel);
        Task<Result<Partida>> FinalizarSegundoTempoProrrogacaoAsync(FimSegundoTempoProrrogacaoViewModel viewModel);
        Task<Result<Partida>> RegistrarDisputaDePenaltisAsync(InicioDisputaPenaltisViewModel viewModel);
        Task<Result<Partida>> FinalizarAsync(FimPartidaViewModel viewModel);
    }
}
