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
        private readonly IValidator<UsuarioLoginDto> _usuarioLoginValidator;

        public UsuarioService(IServiceProvider serviceProvider) : base(serviceProvider) 
        {
            _usuarioCadastroValidator = serviceProvider.GetRequiredService<IValidator<UsuarioCadastroDto>>();
            _usuarioAtualizarValidator = serviceProvider.GetRequiredService<IValidator<UsuarioAtualizarDto>>();
            _usuarioLoginValidator = serviceProvider.GetRequiredService<IValidator<UsuarioLoginDto>>();
        }

        public async Task AtualizarUsuarioAsync(UsuarioAtualizarDto usuarioDto, CancellationToken cancellationToken)
        {
            await _usuarioAtualizarValidator.ValidateAndThrowAsync(usuarioDto); //validações de entrada
            try
            {
                await _unitOfWork.BeginTransactionAsync(); //começando a transação com o UnityOfWork para conciliar as threads e tracks
                var usuario = await _unitOfWork.UsuarioRepository.ObterAsync(
                    c => c.Id == usuarioDto.Id,
                    cancellationToken: cancellationToken
                    );
                //Obtem o usuario atual do banco e o transforma no novo para enviar novamente.
                usuario.Nome = usuarioDto.Nome;
                usuario.Senha = usuarioDto.Password;
                usuario.Email = usuarioDto.Email;
                usuario.DataNascimento = usuarioDto.DataNascimento;
                usuario.DataAtualizacao = DateTime.Now;

                await _unitOfWork.UsuarioRepository.AtualizarAsync(usuario, cancellationToken);
                await _unitOfWork.CommitTransactionAsync();
                return;
            }
            catch (Exception ex) 
            {//Em caso de erro ele para a transação no banco e retorna tanto a mensagem personalizada, quanto a padrão do erro
                await _unitOfWork.RollBackTransactionAsync();
                throw new DatabaseException("Problemas para atualiazar usuário: " + ex.Message, ex);
            }
        }

        public async Task<long> CadastrarUsuarioAsync(UsuarioCadastroDto usuarioDto, CancellationToken cancellationToken)
        {
            await _usuarioCadastroValidator.ValidateAndThrowAsync(usuarioDto);//validações de entrada

            try
            {
                var usuario = new Usuario
                {
                    Nome = usuarioDto.Nome,
                    Email = usuarioDto.Email,
                    Senha = usuarioDto.Password,
                    DataNascimento = usuarioDto.DataNascimento
                };

                var usuarioId = await _unitOfWork.UsuarioRepository.CriarAsync(usuario, cancellationToken);

                return usuarioId;
            }
            catch (Exception ex)
            {//Em caso de erro ele retorna tanto a mensagem personalizada, quanto a padrão do erro
                throw new BusinessException("Erro ao cadastrar usuário. " + ex.Message, ex);
            }
        }

        public async Task<UsuarioRetornoDto> ObterUsuarioAsync(long id, CancellationToken cancellationToken)
        {//obtem a entidade no banco usando um select preparado no nosso repository service
            try
            {
                return await _unitOfWork.UsuarioRepository.ObterAsync<UsuarioRetornoDto>(
                    c => c.Id == id,
                    cancellationToken: cancellationToken
                );
            }
            catch (Exception ex)
            {//Em caso de erro ele retorna tanto a mensagem personalizada, quanto a padrão do erro
                throw new NotFoundException("Usuário não encontrado no banco: " + ex.Message, ex);
            }
        }

        public async Task<List<UsuarioRetornoDto>> ObterUsuariosAsync(CancellationToken cancellationToken)
        {
            try
            {
                var retorno = await _unitOfWork.UsuarioRepository.ObterListaAsync<UsuarioRetornoDto>(
                );

                return retorno.ToList();
            }
            catch (Exception ex)
            {//Em caso de erro ele retorna tanto a mensagem personalizada, quanto a padrão do erro
                throw new NotFoundException("Erro ao obter usuários do banco: " + ex.Message, ex);
            }
        }

        public async Task RemoverUsuarioAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task AutenticarUsuarioAsync(UsuarioLoginDto usuarioDto, CancellationToken cancellationToken)
        {
            await _usuarioLoginValidator.ValidateAndThrowAsync(usuarioDto);

            var existe = await _unitOfWork.UsuarioRepository.ExistsAsync(
            c => c.Email == usuarioDto.Email && c.Senha == usuarioDto.Password,
            cancellationToken
            );

            if(!existe)
                throw new NotFoundException("Email ou senha informados inexistentes.");

            return;
        }

    }
}
