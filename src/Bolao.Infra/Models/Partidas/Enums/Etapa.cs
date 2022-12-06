namespace Bolao.Infra.Models.Partidas.Enums
{
    [Flags]
    public enum Etapa
    {
        PrimeiroTempo = 1,
        SegundoTempo = 2,
        PrimeiroTempoProrrogacao = 4,
        SegundoTempoProrrogacao = 8,
        Penaltis = 16,
        Intervalo = 32,
        NaoIniciada = 64,
        Finalizada = 128
    }
}
