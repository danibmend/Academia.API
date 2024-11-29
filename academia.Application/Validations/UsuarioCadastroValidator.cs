using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Application.DTOs;
using FluentValidation;

namespace academia.Application.Validations
{
    public class UsuarioCadastroValidator : AbstractValidator<UsuarioCadastroDto>
    {
        public UsuarioCadastroValidator()
        {
        }
    }
}
