using System.ComponentModel.DataAnnotations;

namespace Bolao.Application.ViewModels.Partidas
{
    public class AgendamentoPartidaViewModel
    {
        [Required(ErrorMessage = "Local é obrigatório!")]
        public string Local { get; set; }
        public DateTime Data { get; set; }
    }
}
