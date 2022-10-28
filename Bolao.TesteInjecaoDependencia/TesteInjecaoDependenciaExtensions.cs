using Microsoft.Extensions.DependencyInjection;

namespace Bolao.TesteInjecaoDependencia
{
    public static class TesteInjecaoDependenciaExtensions
    {
        public static IServiceCollection AddTesteInjecaoDependencia(this IServiceCollection service)
        {
            service.AddScoped<IOperacaoScoped, OperacaoPadrao>();
            service.AddTransient<IOperacaoTransient, OperacaoPadrao>();
            service.AddSingleton<IOperacaoSingleton>(new OperacaoPadrao());

            return service;
        }
    }
}
