using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace academia.Domain.Entidades.Base
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public Metadados? Metadados { get; set; }
    }

    public class Metadados
    {
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
