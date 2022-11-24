using Bolao.Infra.Models.Partidas;
using Bolao.Infra.Models.Times;
using System.ComponentModel.DataAnnotations;

namespace Bolao.Application.ViewModels.Partidas
{
    public class CadastrarPartidaViewModel
    {
        [Required(ErrorMessage = "Local é obrigatório!")]
        public string Local { get; set; }
        public DateTime Data { get; set; }
        public int AnfitriaoId { get; set; }
        public int VisitanteId { get; set; }
    }

    internal static class PartidaViewModelExtensions
    {
        public static Partida ConvertToModel(this CadastrarPartidaViewModel viewModel)
        {
            return new Partida
            {
                Local = viewModel.Local,
                Data = viewModel.Data,
                Anfitriao = new Time
                {
                    Id = viewModel.AnfitriaoId,
                },
                Visitante = new Time
                {
                    Id = viewModel.VisitanteId,
                }
            };
        }
    }
}
