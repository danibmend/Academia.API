using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using academia.Domain.Interfaces.IRepository;
using academia.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using academia.Infrastructure.Persistence;

namespace academia.Infrastructure.Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _serviceProvider = serviceProvider;
        }

        public ApplicationDbContext Context => _context;
        //inicia a transação no banco de dados
        public async Task BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
        //commita (confirma) a transação no banco de dados
        public async Task CommitTransactionAsync() => await _context.Database.CommitTransactionAsync();
        //desfaz a transação no banco de dados
        public async Task RollBackTransactionAsync() => await _context.Database.RollbackTransactionAsync();

        public void Dispose()
        {
            GC.Collect();
        }

        public IUsuarioRepository UsuarioRepository => _serviceProvider.GetRequiredService<IUsuarioRepository>();
    }
}
