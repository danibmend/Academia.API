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
        {//Injeção de dependencia da classe de serviços do usuario
            _usuarioService = serviceProvider.GetRequiredService<IUsuarioService>();
        }
        //Post
        [HttpPost("usuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioCadastroDto request, CancellationToken cancellationToken)
        {
            var idUsuarioCriado = await _usuarioService.CadastrarUsuarioAsync(request, cancellationToken);
            return Response(idUsuarioCriado);
        }

        //Put
        [HttpPut("usuario")]
        public async Task<IActionResult> AtualizarDadosUsuario([FromBody] UsuarioAtualizarDto request, CancellationToken cancellationToken)
        {
            await _usuarioService.AtualizarUsuarioAsync(request, cancellationToken);
            return Response();
        }

        //Get Identity UNICO
        [HttpGet("usuario/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UsuarioRetornoDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterUsuario([FromRoute] long id, CancellationToken cancellationToken)
        {
            var result = await _usuarioService.ObterUsuarioAsync(id, cancellationToken);
            return Response(result);
        }

        //Get Lista de entidades
        [HttpGet("usuarios")]
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Response se der erro
        [ProducesResponseType(typeof(List<UsuarioRetornoDto>), StatusCodes.Status200OK)] //Response se der tudo ok
        public async Task<IActionResult> ObterUsuarios(CancellationToken cancellationToken)
        {
            var result = await _usuarioService.ObterUsuariosAsync(cancellationToken);
            return Response(result);
        }

        //Validação de Login (Post)
        [HttpPost("usuario/validar")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidarUsuario([FromBody] UsuarioLoginDto request, CancellationToken cancellationToken)
        {
            await _usuarioService.AutenticarUsuarioAsync(request, cancellationToken);
            return Response();
        }
    }
}
