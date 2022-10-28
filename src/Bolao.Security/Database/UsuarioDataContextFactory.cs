using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bolao.Security.Database
{
    public class UsuarioDataContextFactory : IDesignTimeDbContextFactory<UsuarioDataContext>
    {
        public UsuarioDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UsuarioDataContext>();
            //ATENÇÃO: Nunca deixe aqui o banco de dados de produção ou homologação
            //use apenas local
            optionsBuilder.UseSqlServer("Server=localhost,2433;Database=bolao-db;User Id=sa;Password=A123456@;");

            return new UsuarioDataContext(optionsBuilder.Options);
        }

    }
}
