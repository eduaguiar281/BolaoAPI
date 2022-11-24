using System.ComponentModel.DataAnnotations;

namespace Bolao.Application.ViewModels.Partidas.Eventos
{
    public abstract class BaseEventoPartidaViewModel
    {
        [Required(ErrorMessage = "Id da partida é obrigatório!")]
        public int PartidaId { get; set; }
        public string Observacoes { get; set; }
    }
}
