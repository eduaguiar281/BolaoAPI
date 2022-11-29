using Bolao.Application.Interfaces;
using Bolao.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolao.Application
{
    public static class BolaoApplicationExtensions
    {
        public static IServiceCollection AddBolaoApplication(this IServiceCollection service)
        {
            service.AddScoped<ITimesService, TimeService>();
            service.AddScoped<IPartidaService, PartidaService>();
            return service;
        }
    }
}
