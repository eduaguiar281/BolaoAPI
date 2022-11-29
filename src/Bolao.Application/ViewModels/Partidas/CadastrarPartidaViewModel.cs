using Bolao.Infra.Models.Partidas;
using Bolao.Infra.Models.Times;
using System.ComponentModel.DataAnnotations;

namespace Bolao.Application.ViewModels.Partidas
{
    public class CadastrarPartidaViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Local é obrigatório!")]
        public string Local { get; set; }
        public DateTime Data { get; set; }
        public int AnfitriaoId { get; set; }
        public int VisitanteId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AnfitriaoId <= 0)
                yield return new ValidationResult("Id do anfitrião não pode ser igual a zero!", new string[] { nameof(AnfitriaoId) });
            if (VisitanteId <= 0)
                yield return new ValidationResult("Id do visitante não pode ser igual a zero!", new string[] { nameof(VisitanteId) });
            if (Data  <= DateTime.UtcNow)
                yield return new ValidationResult("Você não pode cadastrar partida com data retroativa!", new string[] { nameof(Data) });
            if (VisitanteId == AnfitriaoId)
                yield return new ValidationResult("Visitante e anfitrião possui o mesmo código!", new string[] { nameof(VisitanteId), nameof(AnfitriaoId) });
        }
    }

    internal static class PartidaViewModelExtensions
    {
        public static Partida ConvertToModel(this CadastrarPartidaViewModel viewModel, Time anfitriao, Time visitante)
        {
            return new Partida
            {
                Local = viewModel.Local,
                Data = viewModel.Data,
                Anfitriao = anfitriao,
                Visitante = visitante
            };
        }

    }
}
