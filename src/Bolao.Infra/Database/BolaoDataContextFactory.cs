using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bolao.Infra.Database
{
    public class BolaoDataContextFactory : IDesignTimeDbContextFactory<BolaoDataContext>
    {
        public BolaoDataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BolaoDataContext>();
            //ATENÇÃO: Nunca deixe aqui o banco de dados de produção ou homologação
            //use apenas local
            optionsBuilder.UseSqlServer("Server=localhost,2433;Database=bolao-db;User Id=sa;Password=A123456@;Encrypt=True;TrustServerCertificate=True;");

            return new BolaoDataContext(optionsBuilder.Options);
        }
    }
}
