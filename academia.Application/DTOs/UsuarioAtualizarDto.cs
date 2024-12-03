using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace academia.Application.DTOs
{
    public class UsuarioAtualizarDto
    {
        //Model Request de atualizar usuario
        public long Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
