using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Application.Interfaces.Validator;
using academia.Application.Services.Base;

namespace academia.Application.Validations.Utils
{
    public class UtilsValidator : ServiceBase, IUtilsValidator
    {
        public UtilsValidator(IServiceProvider serviceProvider) : base(serviceProvider) 
        { 
        }

        public async Task<bool> NomeUsuarioExistenteAsync(string? nome, CancellationToken cancellationToken)
        {
           var result = await _unitOfWork.UsuarioRepository.ExistsAsync(
                c => c.Nome == nome,
                cancellationToken
                );

            return !result;
        }
    }
}
