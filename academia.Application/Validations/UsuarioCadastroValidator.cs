﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
