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

        public async Task<bool> EmailExistenteAsync(string? email, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.UsuarioRepository.ExistsAsync(
                 c => c.Email == email,
                 cancellationToken
                 );

            return !result;
        }

        public async Task<bool> NomeUsuarioExistenteAtualizarAsync(string? nome, long id, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.UsuarioRepository.ExistsAsync(
                 c => c.Nome == nome && c.Id != id,
                 cancellationToken
                 );

            return !result;
        }

        public async Task<bool> EmailExistenteAtualizarAsync(string? email, long id, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.UsuarioRepository.ExistsAsync(
                 c => c.Email == email && c.Id != id,
                 cancellationToken
                 );

            return !result;
        }

        public bool EmailValido(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
