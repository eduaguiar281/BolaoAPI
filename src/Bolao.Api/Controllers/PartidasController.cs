using Bolao.Application.Interfaces;
using Bolao.Application.Services;
using Bolao.Application.ViewModels.Partidas;
using Bolao.Application.ViewModels.Partidas.Eventos;
using Bolao.Infra.Models.Partidas;
using Bolao.Infra.Models.Times;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bolao.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PartidasController : ApiControllerBase
    {
        private readonly IPartidaService _partidaService;
        public PartidasController(IPartidaService partidaService)
        {
            _partidaService = partidaService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(CadastrarPartidaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Result<Partida> result = await _partidaService.IncluirAsync(viewModel);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetById), new { result.Value.Id }, result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("cancelar")]
        public async Task<IActionResult> Cancelar(CancelarPartidaViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Result<Partida> result = await _partidaService.CancelarAsync(viewModel);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Result<Partida> result = await _partidaService.ObterPorIdAsync(id);
            if (result.IsFailure)
                return result.Error.Contains("não foi localizado!") ? NotFound(result.Error) : BadRequest(result.Error);
            return Ok(result.Value);
        }

    }
}
