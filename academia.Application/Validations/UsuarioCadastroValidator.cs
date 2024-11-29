﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Application.DTOs;
using academia.Application.Interfaces.Validator;
using FluentValidation;

namespace academia.Application.Validations
{
    public class UsuarioCadastroValidator : AbstractValidator<UsuarioCadastroDto>
    {
        private readonly IUtilsValidator _utilsValidator;
        public UsuarioCadastroValidator(IUtilsValidator utilsValidator)
        {
            _utilsValidator = utilsValidator;

            RuleFor(usuario => usuario.Senha)
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
                .MustAsync(_utilsValidator.NomeUsuarioExistenteAsync)
                .WithMessage("Nome de usuário já em uso.")
                .When(c => c.Nome != null);
        }
    }
}
