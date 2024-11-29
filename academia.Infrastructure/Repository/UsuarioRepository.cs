using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Domain.Entidades;
using academia.Domain.Interfaces.IRepository;
using academia.Infrastructure.Repository.Base;

namespace academia.Infrastructure.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
