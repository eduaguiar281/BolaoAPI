namespace Bolao.Application.ViewModels.Partidas.Filtros
{
    public class FiltroListarPartidasViewModel
    {
        public DateTime? DataIncio { get; set; }
        public DateTime? DataFim { get; set; }
        public int? TimeId { get; set; }
        public string Local { get; set; }
    }
}
