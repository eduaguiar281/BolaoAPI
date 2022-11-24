using System.ComponentModel.DataAnnotations;

namespace Bolao.Application.ViewModels.Partidas.Eventos
{
    public abstract class BaseEventoPartidaJogadorViewModel: BaseEventoPartidaViewModel
    {
        [Required(ErrorMessage ="Nome do jogador é obrigatório!")]
        public string NomeJogador { get; set; }
        public ushort Minuto { get; set; }
        public bool EhEventoDoAnfitriao { get; set; } = true;
    }
}
