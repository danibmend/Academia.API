using academia.Application.DTOs;
using academia.Application.Interfaces;
using academia.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace academia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _usuarioService = serviceProvider.GetRequiredService<IUsuarioService>();
        }
        [HttpPost("usuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioCadastroDto request, CancellationToken cancellationToken)
        {
            await _usuarioService.CadastrarUsuarioAsync(request, cancellationToken);
            return Response();
        }

        [HttpPut("usuario")]
        public async Task<IActionResult> AtualizarDadosUsuario([FromBody] UsuarioAtualizarDto request, CancellationToken cancellationToken)
        {
            await _usuarioService.AtualizarUsuarioAsync(request, cancellationToken);
            return Response(null);
        }

        [HttpGet("usuario/{cpf}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UsuarioRetornoDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterUsuario([FromRoute] string cpf, CancellationToken cancellationToken)
        {
            var result = await _usuarioService.ObterPorCPFAsync(cpf, cancellationToken);
            return Response(result);
        }

        [HttpGet("usuario/validar")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidarUsuario([FromBody] UsuarioLoginDto request, CancellationToken cancellationToken)
        {
            var result = await _usuarioService.ValidarUsuarioAsync(request, cancellationToken);
            return Response(result);
        }
    }
}
