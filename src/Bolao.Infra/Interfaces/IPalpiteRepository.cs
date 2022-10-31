
using Bolao.Infra.Models.Partidas;

namespace Bolao.Infra.Interfaces
{

    //TODO: Este repositório não deve ser levado em consideração. 
    //Vou demonstrar o que não fazer
    public interface IPalpiteRepository
    {
        Task DeleteAsync(Palpite palpite);
        Task InsertAsync(Palpite palpite);
        Task UpdateAsync(Palpite palpite);
        Task<Palpite> ObterPorIdAsync(int id);
        Task<IEnumerable<Palpite>> ObterPorPartidaIdAsync(int partidaId);
        Task<IEnumerable<Palpite>> ObterPorUsuarioAsync(string nomeUsuario);
    }
}
