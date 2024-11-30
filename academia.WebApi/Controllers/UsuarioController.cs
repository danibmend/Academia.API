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
            var idUsuarioCriado = await _usuarioService.CadastrarUsuarioAsync(request, cancellationToken);
            return Response(idUsuarioCriado);
        }

        [HttpPut("usuario")]
        public async Task<IActionResult> AtualizarDadosUsuario([FromBody] UsuarioAtualizarDto request, CancellationToken cancellationToken)
        {
            await _usuarioService.AtualizarUsuarioAsync(request, cancellationToken);
            return Response();
        }

        [HttpGet("usuario/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UsuarioRetornoDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterUsuario([FromRoute] long id, CancellationToken cancellationToken)
        {
            var result = await _usuarioService.ObterUsuarioAsync(id, cancellationToken);
            return Response(result);
        }

        [HttpGet("usuario/validar/nome/{nome}/senha/{senha}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidarUsuario([FromRoute] string nome, string senha, CancellationToken cancellationToken)
        {
            await _usuarioService.AutenticarUsuarioAsync(nome, senha, cancellationToken);
            return Response();
        }
    }
}
