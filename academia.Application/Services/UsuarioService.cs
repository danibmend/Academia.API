using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Application.DTOs;
using academia.Application.Interfaces;
using academia.Application.Services.Base;
using academia.Domain.Entidades;
using academia.Domain.Exceptions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace academia.Application.Services
{
    public class UsuarioService : ServiceBase, IUsuarioService
    {
        private readonly IValidator<UsuarioCadastroDto> _usuarioCadastroValidator;
        private readonly IValidator<UsuarioAtualizarDto> _usuarioAtualizarValidator;

        public UsuarioService(IServiceProvider serviceProvider) : base(serviceProvider) 
        {
            _usuarioCadastroValidator = serviceProvider.GetRequiredService<IValidator<UsuarioCadastroDto>>();
            _usuarioAtualizarValidator = serviceProvider.GetRequiredService<IValidator<UsuarioAtualizarDto>>();
        }

        public async Task AtualizarUsuarioAsync(UsuarioAtualizarDto usuarioDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<long> CadastrarUsuarioAsync(UsuarioCadastroDto usuarioDto, CancellationToken cancellationToken)
        {
            await _usuarioCadastroValidator.ValidateAndThrowAsync(usuarioDto);

            try
            {
                var usuario = new Usuario
                {
                    Nome = usuarioDto.Nome,
                    Email = usuarioDto.Email,
                    Senha = usuarioDto.Senha,
                    DataNascimento = usuarioDto.DataNascimento
                };

                var usuarioId = await _unitOfWork.UsuarioRepository.CriarAsync(usuario, cancellationToken);

                return usuarioId;
            }
            catch (Exception ex) 
            {
                throw new BusinessException("Erro ao cadastrar usuário. " + ex.Message, ex);
            }
        }

        public async Task<UsuarioRetornoDto> ObterUsuarioAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task RemoverUsuarioAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task AutenticarUsuarioAsync(string nome, string senha, CancellationToken cancellationToken)
        {
                var existe = await _unitOfWork.UsuarioRepository.ExistsAsync(
                c => c.Nome == nome && c.Senha == senha,
                cancellationToken
                );

                if(!existe)
                    throw new NotFoundException("Usuário ou senha informados inexistentes.");

                return;
        }
    }
}
