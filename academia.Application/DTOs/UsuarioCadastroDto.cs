﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace academia.Application.DTOs
{
    public class UsuarioCadastroDto
    {
        //Model Request de cadastrar usuario
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
