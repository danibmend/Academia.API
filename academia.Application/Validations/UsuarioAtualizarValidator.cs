using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Application.DTOs;
using academia.Application.Interfaces.Validator;
using FluentValidation;

namespace academia.Application.Validations
{
    public class UsuarioAtualizarValidator : AbstractValidator<UsuarioAtualizarDto>
    {
        private readonly IUtilsValidator _utilsValidator;
        public UsuarioAtualizarValidator(IUtilsValidator utilsValidator)
        {
            _utilsValidator = utilsValidator;

            RuleFor(usuario => usuario.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Senha de usuário deve ser informada.");

            RuleFor(usuario => usuario.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome de usuário deve ser informado.");

            RuleFor(usuario => usuario.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email de usuário deve ser informado.");

            RuleFor(usario => usario.Nome)
                .MustAsync((o, orgao, CancellationToken) =>
                {
                    return _utilsValidator.NomeUsuarioExistenteAtualizarAsync(o.Nome!, o.Id, CancellationToken);
                })
                .WithMessage("Nome de usuário já em uso.")
                .When(c => c.Nome != null);

            RuleFor(usario => usario.Email)
                .MustAsync((o, orgao, CancellationToken) =>
                {
                    return _utilsValidator.EmailExistenteAtualizarAsync(o.Email!, o.Id, CancellationToken);
                })
                .WithMessage("Email já em uso.")
                .When(c => c.Email != null);

            RuleFor(usario => usario.Email)
                .Must(_utilsValidator.EmailValido)
                .WithMessage("Formato de email invalido.")
                .When(c => c.Email != null);
        }
    }
}