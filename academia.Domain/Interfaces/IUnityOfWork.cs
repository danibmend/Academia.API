using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Domain.Interfaces.IRepository;

namespace academia.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUsuarioRepository UsuarioRepository { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        void Dispose();
        Task RollBackTransactionAsync();
    }
}
