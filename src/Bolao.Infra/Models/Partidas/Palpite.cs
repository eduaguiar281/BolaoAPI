using Bolao.Core.Models;
using Bolao.Infra.Models.Partidas.Enums;

namespace Bolao.Infra.Models.Partidas
{
    public class Palpite : Entidade<int>
    {
        public Palpite()
        {
            GolsAnfitriao = 0;
            GolsVisitante = 0;
            ResultadoFinal = ResultadoPartida.Empate;
        }
        public string NomeUsuario { get; set; }
        public Partida Partida { get; set; }
        public ushort GolsAnfitriao { get; set; }
        public ushort GolsVisitante { get; set; }
        public ResultadoPartida ResultadoFinal { get; set; }
    }
}
