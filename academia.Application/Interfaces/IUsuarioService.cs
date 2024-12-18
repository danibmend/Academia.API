﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Application.DTOs;

namespace academia.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<long> CadastrarUsuarioAsync(UsuarioCadastroDto usuarioDto, CancellationToken cancellationToken);
        Task AtualizarUsuarioAsync(UsuarioAtualizarDto usuarioDto, CancellationToken cancellationToken);
        Task RemoverUsuarioAsync(long id, CancellationToken cancellationToken);
        Task<UsuarioRetornoDto> ObterUsuarioAsync(long id, CancellationToken cancellationToken);
        Task<List<UsuarioRetornoDto>> ObterUsuariosAsync(CancellationToken cancellationToken);
        Task AutenticarUsuarioAsync(UsuarioLoginDto usuarioDto, CancellationToken cancellationToken);
    }
}
