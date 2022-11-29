using Bolao.Core.Models;
using Bolao.Infra.Models.Partidas.Enums;
using Bolao.Infra.Models.Times;

namespace Bolao.Infra.Models.Partidas
{
    public class HistoricoPartida : Entidade<int>
    {
        public Time Time { get; set; }
        public ushort? Minuto { get; set; }
        public string Jogador { get; set; }
        public Evento Evento { get; set; }
        public string Observacoes { get; set; }
    }
}
