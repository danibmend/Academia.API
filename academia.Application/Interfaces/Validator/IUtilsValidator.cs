using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace academia.Application.Interfaces.Validator
{
    public interface IUtilsValidator
    {
        Task<bool> NomeUsuarioExistenteAsync(string? nome, CancellationToken cancellationToken);
    }
}
