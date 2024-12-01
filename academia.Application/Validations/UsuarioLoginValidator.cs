using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Application.DTOs;
using FluentValidation;

namespace academia.Application.Validations
{
    public class UsuarioLoginValidator : AbstractValidator<UsuarioLoginDto>
    {
        public UsuarioLoginValidator() 
        {
            RuleFor(usuario => usuario.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Senha de usuário deve ser informada.");

            RuleFor(usuario => usuario.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email de usuário deve ser informado.");
        }
    }
}
