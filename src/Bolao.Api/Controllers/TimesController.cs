using Bolao.Application.Interfaces;
using Bolao.Infra.Models.Times;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bolao.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TimesController : ApiControllerBase
    {
        private readonly ITimesService _timesService;
        public TimesController(ITimesService timesService)
        {
            _timesService = timesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Result<IEnumerable<Time>> results = await _timesService.ListarTodosAsync();
            if (results.IsFailure)
                return BadRequest(results.Error);
            return Ok(results.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Result<Time> result = await _timesService.ObterPorIdAsync(id);
            if (result.IsFailure)
                return result.Error.Contains("não foi localizado!") ? NotFound(result.Error) : BadRequest(result.Error);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(Time time)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Result<Time> result = await _timesService.IncluirAsync(time);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetById), new { Id = result.Value.Id }, result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put(Time time)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Result<Time> result = await _timesService.AlterarAsync(time);
            if (result.IsFailure)
                return result.Error.Contains("não foi localizado!") ? NotFound(result.Error) : BadRequest(result.Error);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Result result = await _timesService.ExcluirAsync(id);
            if (result.IsFailure)
                return result.Error.Contains("não foi localizado!") ? NotFound(result.Error) : BadRequest(result.Error);
            return Ok();
        }
    }
}
