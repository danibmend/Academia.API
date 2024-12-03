using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Domain.Entidades.Base;

namespace academia.Domain.Entidades
{
    public class Usuario : BaseEntity
    {
        //entidade de usuario
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
