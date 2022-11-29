using Bolao.Core.Models;
using Bolao.Infra.Models.Partidas.Enums;
using Bolao.Infra.Models.Times;

namespace Bolao.Infra.Models.Partidas
{
    public class Partida : Entidade<int>
    {
        public Partida()
        {
            GolsAnfitriao = 0;
            GolsVisitante = 0;
            Resultado = ResultadoPartida.Empate;
            Etapa = Etapa.NaoIniciada;
        }
        public string Local { get; set; }
        public DateTime Data { get; set; }
        //TODO: Mostrar o conceito de Value Object

        public Time Anfitriao { get; set; }
        public Time Visitante { get; set; }

        public ushort GolsAnfitriao { get; set; }
        public ushort GolsVisitante { get; set; }
        public ResultadoPartida Resultado { get; set; }
        //TODO: Mostrar o conceito de Value Object

        //TODO: Modificar para ReadonlyCollection
        public List<HistoricoPartida> Historicos { get; set; }
        public List<Palpite> Palpites { get; set; }

        public Etapa Etapa { get; set; }

        public DateTime? DataCancelamento { get; set; }
         
    }
}
