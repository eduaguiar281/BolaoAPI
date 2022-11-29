using Bolao.Infra.Database;
using Bolao.Infra.Interfaces;
using Bolao.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bolao.Infra
{
    public static class BolaoInfraExtensions
    {
        public static IServiceCollection AddBolaoInfra(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<BolaoDataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            service.AddScoped<ITimeRepository, TimeRepository>();
            service.AddScoped<IPartidaRepository, PartidaRepository>();
            return service;
        }

    }
}
