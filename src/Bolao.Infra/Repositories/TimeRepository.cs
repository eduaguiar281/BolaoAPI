using Bolao.Infra.Database;
using Bolao.Infra.Interfaces;
using Bolao.Infra.Models.Times;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolao.Infra.Repositories
{
    public class TimeRepository : ITimeRepository
    {
        private readonly BolaoDataContext _bolaoDataContext;

        public TimeRepository(BolaoDataContext bolaoDataContext)
        {
            _bolaoDataContext = bolaoDataContext;
        }

        public async Task DeleteAsync(Time time)
        {
            if (time is null)
                throw new ArgumentNullException(nameof(time), "Nenhum time foi informado!");
            _bolaoDataContext.Times.Remove(time);
            await _bolaoDataContext.SaveChangesAsync();
        }

        public async Task InsertAsync(Time time)
        {
            if (time is null)
                throw new ArgumentNullException(nameof(time), "Nenhum time foi informado!");
            _bolaoDataContext.Times.Add(time);
            await _bolaoDataContext.SaveChangesAsync();
        }

        public async Task<Time> ObterPorIdAsync(int id)
        {
            return await _bolaoDataContext.Times.FindAsync(id);
        }

        public async Task<IEnumerable<Time>> ObterTodosAsync()
        {
            return await _bolaoDataContext.Times.ToListAsync();
        }

        public async Task UpdateAsync(Time time)
        {
            if (time is null)
                throw new ArgumentNullException(nameof(time), "Nenhum time foi informado!");
            _bolaoDataContext.Times.Update(time);
            await _bolaoDataContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Time>> ObterPorNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentNullException(nameof(nome), "Nome do time não foi informado!");
            return await _bolaoDataContext.Times
                .Where(time => time.Nome.ToUpper() == nome.ToUpper() )
                .ToListAsync();
        }
    }
}
