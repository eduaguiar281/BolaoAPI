﻿using Bolao.Core.Models;
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
            Finalizado = false;
        }
        public string Local { get; set; }
        public DateTime Data { get; set; }
        public Time Anfitriao { get; set; }
        public Time Visitante { get; set; }
        public ushort GolsAnfitriao { get; set; }
        public ushort GolsVisitante { get; set; }
        public bool Finalizado { get; set; }
        public ResultadoPartida Resultado { get; set; }
        public List<HistoricoPartida> Historicos { get; set; }
        public List<Palpite> Palpites { get; set; }
    }
}
