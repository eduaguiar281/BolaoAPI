using Bolao.Infra.Models.Times;
using Bolao.TesteInjecaoDependencia;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bolao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteDIController : ApiControllerBase
    {
        private readonly IOperacaoScoped _operacaoScoped;
        private readonly IOperacaoSingleton _operacaoSingleton;
        private readonly IOperacaoTransient _operacaoTransient;
        public TesteDIController(IOperacaoScoped operacaoScoped, IOperacaoSingleton operacaoSingleton, IOperacaoTransient operacaoTransient)
        {
            _operacaoScoped = operacaoScoped;
            _operacaoSingleton = operacaoSingleton;
            _operacaoTransient = operacaoTransient;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var operacaoScoped = HttpContext.RequestServices.GetRequiredService<IOperacaoScoped>();
            var operacaoSingleton = HttpContext.RequestServices.GetRequiredService<IOperacaoSingleton>();
            var operacaoTransient = HttpContext.RequestServices.GetRequiredService<IOperacaoTransient>();

            return Ok( new
            {
                Scope1 = new
                {
                    Scoped = _operacaoScoped.IdOperacao,
                    Transient = _operacaoTransient.IdOperacao,
                    Singleton = _operacaoSingleton.IdOperacao
                },
                Scope2 = new
                {
                    Scoped = operacaoScoped.IdOperacao,
                    Transient = operacaoTransient.IdOperacao,
                    Singleton = operacaoSingleton.IdOperacao
                }
            });
        }

    }
}
