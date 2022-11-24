using System.ComponentModel.DataAnnotations;

namespace Bolao.Application.ViewModels
{
    //TODO: Mudar Controller e Service para usar esta ViewModel

    public class TimeViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Nome { get; set; }

    }
}
