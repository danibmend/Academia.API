using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Application.Interfaces;

namespace academia.Application.Services
{
    public class UsuarioService : ServiceBase, IUsuarioService
    {
        private readonly IValidator<UsuarioCadastroDto> _usuarioCadastroValidator;
        private readonly IValidator<UsuarioAtualizarDto> _usuarioAtualizarValidator;

        public UsuarioService(IServiceProvider serviceProvider) : base(serviceProvider) 
        {
            _usuarioCadastroValidator = serviceProvider.GetRequiredService<IValidator<UsuarioCadastroDto>>();
            _usuarioAtualizarValidator = serviceProvider.GetRequiredService<IValidator<UsuarioAtualizarDto>>();
        }
    }
}
