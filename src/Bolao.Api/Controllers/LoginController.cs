using Bolao.Security.Interface;
using Bolao.Security.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bolao.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LoginController : ApiControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Token([FromForm] IFormCollection form)
        {
            if (!form.ContainsKey("username") || !form.ContainsKey("password"))
                return BadRequest("Usuário ou senha não foi informado!");
            Result<TokenDto> result = await _usuarioService.LoginAsync(form["username"], form["password"]);
            return result.IsSuccess ? Ok(result.Value) : Unauthorized(result.Error);
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken(TokenDto tokenDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Result<TokenDto> result = await _usuarioService.RefreshTokenAsync(tokenDto);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPatch("revoke/{username}")]
        public async Task<IActionResult> RevokeRefreshToken(string username)
        {
            Result result = await _usuarioService.RevokeAsync(username);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpPatch("revokeall")]
        public async Task<IActionResult> RevokeAll()
        {
            await _usuarioService.RevokeAllAsync();
            return Ok();
        }
    }
}
