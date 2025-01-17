﻿namespace Bolao.Infra.Models.Partidas.Enums
{
    public enum Evento
    {
        Gol = 0,
        GolAnulado = 1,
        CartaoVermelho = 2,
        CartaoAmarelo = 3,
        Falta = 4, 
        InicioPartida = 5,
        FimPartida = 6,
        InicioEtapa = 7,
        FimEtapa = 8, 
        Cancelamento = 9,
        /*Disputa de Penaltis*/
        PenaltiConvertido = 10,
        PenaltiPerdido =11,
        /*Disputa de Penaltis*/
    }
}
