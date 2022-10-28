using Bolao.Infra.Models.Partidas.Enums;

namespace Bolao.Infra.Models.Partidas
{
    public class Palpite
    {
        public Palpite()
        {
            GolsAnfitriao = 0;
            GolsVisitante = 0;
            ResultadoFinal = ResultadoPartida.Empate;
        }
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public Partida Partida { get; set; }
        public ushort GolsAnfitriao { get; set; }
        public ushort GolsVisitante { get; set; }
        public ResultadoPartida ResultadoFinal { get; set; }
    }
}
