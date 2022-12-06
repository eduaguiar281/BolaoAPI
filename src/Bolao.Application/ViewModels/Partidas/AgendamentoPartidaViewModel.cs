using Bolao.Application.ViewModels.Partidas.Eventos;
using System.ComponentModel.DataAnnotations;

namespace Bolao.Application.ViewModels.Partidas
{
    public class AgendamentoPartidaViewModel : IValidatableObject
    {
        public int PartidaId { get; set; }
        [Required(ErrorMessage = "Local é obrigatório!")]
        public string Local { get; set; }
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "É obrigatório informar as observações!")]
        public string Observacoes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Data <= DateTime.UtcNow)
                yield return new ValidationResult("Data/hora da partida não pode ser menor ou igual a data/hora atual!", new string[] { nameof(Data) });
        }
    }
}
