using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using academia.Domain.Entidades.Base;

namespace academia.Domain.Interfaces.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<long> CriarAsync(TEntity obj, CancellationToken cancellationToken = default);
        Task CriarAsync(TEntity[] obj, CancellationToken cancellationToken = default);
        Task AtualizarAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AtualizarAsync(TEntity[] entity, CancellationToken cancellationToken = default);
        Task RemoverAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task RemoverAsync(TEntity[] obj, CancellationToken cancellationToken = default);
        Task<IEnumerable<TDto>> ObterListaAsync<TDto>(
                       CancellationToken cancellationToken = default);
        

            Task<IEnumerable<TDto>> ObterListaAsync<TDto>(
            Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity, TDto>> select,
            string? includes = null,
            string? orderBy = null,
            int? pageSize = null,
            int? page = null,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> ObterListaAsync(
            Expression<Func<TEntity, bool>> expression,
            string? includes = null,
            string? orderBy = null,
            int? pageSize = null,
            int? page = null,
            CancellationToken cancellationToken = default);

        Task<TEntity> ObterPorIdAsync(long id, string? includes = null, CancellationToken cancellationToken = default);
        Task<TDto> ObterPorIdAsync<TDto>(long id, string? includes = null, CancellationToken cancellationToken = default);

        [Obsolete]
        Task<IEnumerable<TEntity>> ObterTodos(CancellationToken cancellationToken = default);
        Task<TDto> ObterAsync<TDto>(
           Expression<Func<TEntity, bool>> expression,
           string? includes = null,
           string? orderBy = null,
           CancellationToken cancellationToken = default);

        Task<TDto> ObterAsync<TDto>(
           Expression<Func<TEntity, bool>> expression,
           Expression<Func<TEntity, TDto>> select,
           string? includes = null,
           string? orderBy = null,
           CancellationToken cancellationToken = default);

        Task<TEntity> ObterAsync(
            Expression<Func<TEntity, bool>> expression,
            string? includes = null,
            string? orderBy = null,
            CancellationToken cancellationToken = default);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellation);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellation);
    }
}
