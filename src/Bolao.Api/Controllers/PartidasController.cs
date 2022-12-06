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
        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> Cancelar([FromRoute]int id, [FromQuery]string observacoes)
        {
            Result<Partida> result = await _partidaService.CancelarAsync(new CancelarPartidaViewModel
            {
                PartidaId= id,
                Observacoes= observacoes
            });
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/iniciar")]
        public async Task<IActionResult> Iniciar([FromRoute] int id, [FromQuery] string observacoes)
        {
            Result<Partida> result = await _partidaService.IniciarAsync(new InicioPartidaViewModel
            {
                PartidaId = id,
                Observacoes = observacoes
            });
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/iniciarsegundotempo")]
        public async Task<IActionResult> IniciarSegundoTempo([FromRoute] int id, [FromQuery] string observacoes)
        {
            Result<Partida> result = await _partidaService.IniciarSegundoTempoAsync(new InicioSegundoTempoViewModel
            {
                PartidaId = id,
                Observacoes = observacoes
            });
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/agendamento")]
        public async Task<IActionResult> Agendamento([FromRoute] int id, [FromBody] AgendamentoPartidaViewModel viewModel)
        {
            viewModel.PartidaId = id;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Result<Partida> result = await _partidaService.ModificarAgendamentoAsync(viewModel);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/registrargol")]
        public async Task<IActionResult> RegistrarGol([FromRoute] int id, [FromBody] RegistroGolViewModel viewModel)
        {
            viewModel.PartidaId = id;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Result<Partida> result = await _partidaService.RegistrarGolAsync(viewModel);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/anulargol")]
        public async Task<IActionResult> AnularGol([FromRoute] int id, [FromBody] AnularGolViewModel viewModel)
        {
            viewModel.PartidaId = id;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Result<Partida> result = await _partidaService.AnularGolAsync(viewModel);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/registrarcartaovermelho")]
        public async Task<IActionResult> RegistrarCartaoVermelho([FromRoute] int id, [FromBody] RegistrarCartaoVermelhoViewModel viewModel)
        {
            viewModel.PartidaId = id;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Result<Partida> result = await _partidaService.RegistrarCartaoVermelhoAsync(viewModel);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/registrarcartaoamarelo")]
        public async Task<IActionResult> RegistrarCartaoAmarelo([FromRoute] int id, [FromBody] RegistrarCartaoAmareloViewModel viewModel)
        {
            viewModel.PartidaId = id;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Result<Partida> result = await _partidaService.RegistrarCartaoAmareloAsync(viewModel);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/finalizarprimeirotempo")]
        public async Task<IActionResult> FinalizarPrimeiroTempo([FromRoute] int id, [FromBody] FimPrimeiroTempoViewModel viewModel)
        {
            viewModel.PartidaId = id;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Result<Partida> result = await _partidaService.FinalizarPrimeiroTempoAsync(viewModel);
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
