using System.ComponentModel.DataAnnotations;

namespace Bolao.Application.ViewModels.Partidas.Eventos
{
    public class RegistrarCartaoAmareloViewModel : BaseEventoPartidaJogadorViewModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Observacoes))
                yield return new ValidationResult("É obrigatório informar as observações!", new string[] { nameof(Observacoes) });
        }
    }
}
