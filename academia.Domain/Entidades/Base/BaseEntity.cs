using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace academia.Domain.Entidades.Base
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataAtualizacao { get; set; } = DateTime.Now;
    }
}
