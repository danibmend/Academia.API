using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace academia.Infrastructure.Repository.Base
{
    public class RepositoryBase
    {
        public abstract class RepositoryBase<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
        {
            private PortalServicoContext _context { get; set; }
            private IMapper _mapper;
            private MapperConfiguration _mapperConfiguration;
            public RepositoryBase(IServiceProvider serviceProvider)
            {
                _context = serviceProvider.GetRequiredService<PortalServicoContext>();
                _mapper = serviceProvider.GetRequiredService<IMapper>();
                _mapperConfiguration = (MapperConfiguration)_mapper.ConfigurationProvider;
            }

            public async Task<bool> VerificarConexaoAsync() => await _context.Database.CanConnectAsync();

            public async Task<IQueryable<TEntity>> ObterConsulta()
            {
                return await Task.Run(() => _context.Set<TEntity>().AsQueryable());
            }

            #region Métodos de Criação

            public async Task<long> CriarAsync(TEntity obj, CancellationToken cancellationToken = default)
            {
                try
                {
                    await _context.Set<TEntity>().AddAsync(obj);
                    await _context.SaveChangesAsync(cancellationToken = default);
                    return obj.Id;
                }
                catch (Exception ex)
                {
                    _context.ChangeTracker.Clear();
                    throw ex;
                }
            }

            public async Task CriarAsync(TEntity[] obj, CancellationToken cancellationToken = default)
            {
                try
                {
                    foreach (var entity in obj)
                    {
                        await _context.Set<TEntity>().AddAsync(entity);
                    }
                    await _context.SaveChangesAsync(cancellationToken = default);
                    return;
                }
                catch (Exception ex)
                {
                    _context.ChangeTracker.Clear();
                    throw ex;
                }
            }

            public async Task AtualizarAsync(TEntity entity, CancellationToken cancellationToken = default)
            {
                try
                {
                    _context.Set<TEntity>().Update(entity);
                    await _context.SaveChangesAsync(cancellationToken = default);
                    return;
                }
                catch (Exception ex)
                {
                    _context.ChangeTracker.Clear();
                    throw ex;
                }
            }

            public async Task AtualizarAsync(TEntity[] entity, CancellationToken cancellationToken = default)
            {
                try
                {
                    _context.Set<TEntity>().UpdateRange(entity);
                    await _context.SaveChangesAsync(cancellationToken = default);
                    return;
                }
                catch (Exception ex)
                {
                    _context.ChangeTracker.Clear();
                    throw ex;
                }
            }

            public async Task RemoverAsync(TEntity entity, CancellationToken cancellationToken = default)
            {
                try
                {
                    _context.Remove(entity);
                    await _context.SaveChangesAsync(cancellationToken = default);
                    return;
                }
                catch (Exception ex)
                {
                    _context.ChangeTracker.Clear();
                    throw ex;
                }
            }

            public async Task RemoverAsync(TEntity[] obj, CancellationToken cancellationToken = default)
            {
                try
                {
                    foreach (var entity in obj)
                    {
                        _context.Remove(entity);
                    }
                    await _context.SaveChangesAsync(cancellationToken = default);
                    return;
                }
                catch (Exception ex)
                {
                    _context.ChangeTracker.Clear();
                    throw ex;
                }
            }

            #endregion

            #region Métodos de Listagem

            public async Task<IEnumerable<TDto>> ObterListaAsync<TDto>(
                Expression<Func<TEntity, bool>> expression,
                string? includes = null,
                string? orderBy = null,
                int? pageSize = null, int? page = null,
                CancellationToken cancellationToken = default)
            {
                var set = _context.Set<TEntity>().AsQueryable();
                set = ApplyIncludes(set, includes);
                set = ApplyFilter(set, expression);
                set = ApplySort(set, orderBy);
                set = ApplyPagination(set, pageSize, page);


                var result = await set
                    .AsNoTracking()
                    .ProjectTo<TDto>(_mapperConfiguration)
                    .ToListAsync(cancellationToken);



                return result;
            }

            public async Task<IEnumerable<TDto>> ObterListaAsync<TDto>(
                Expression<Func<TEntity, bool>> expression,
                Expression<Func<TEntity, TDto>> select,
                string? includes = null,
                string? orderBy = null,
                int? pageSize = null, int? page = null,
                CancellationToken cancellationToken = default)
            {
                var set = _context.Set<TEntity>().AsQueryable();
                set = ApplyIncludes(set, includes);
                set = ApplyFilter(set, expression);
                set = ApplySort(set, orderBy);
                set = ApplyPagination(set, pageSize, page);


                var result = await set
                    .AsNoTracking()
                    .Select(select)
                    .ToListAsync(cancellationToken);


                return result;

            }


            public async Task<IEnumerable<TEntity>> ObterListaAsync(
                Expression<Func<TEntity, bool>> expression,
                string? includes = null,
                string? orderBy = null,
                int? pageSize = null, int? page = null,
                CancellationToken cancellationToken = default)
            {
                var set = _context.Set<TEntity>().AsQueryable();
                set = ApplyIncludes(set, includes);
                set = ApplyFilter(set, expression);
                set = ApplySort(set, orderBy);
                set = ApplyPagination(set, pageSize, page);

                var result = await set
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);



                return result;
            }

            [Obsolete]
            public async Task<IEnumerable<TEntity>> ObterTodos(CancellationToken cancellationToken = default)
            {
                return await _context.Set<TEntity>().ToListAsync(cancellationToken);
            }

            public async Task<TEntity> ObterPorIdAsync(long id, string? includes = null, CancellationToken cancellationToken = default)
            {
                var set = _context.Set<TEntity>().AsQueryable();
                set = ApplyIncludes(set, includes);
                var result = await set
                    .Where(c => c.Id == id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);

                if (result == null)
                    throw new NotFoundException("Registro não encontrado no banco de dados.");

                return result;

            }
            public async Task<TDto> ObterPorIdAsync<TDto>(long id, string? includes = null, CancellationToken cancellationToken = default)
            {
                var set = _context.Set<TEntity>().AsQueryable();
                set = ApplyIncludes(set, includes);
                var result = await set
                    .Where(c => c.Id == id)
                    .AsNoTracking()
                    .ProjectTo<TDto>(_mapperConfiguration)
                    .FirstOrDefaultAsync(cancellationToken);

                if (result == null)
                    throw new NotFoundException("Registro não encontrado no banco de dados.");

                return result;
            }

            public async Task<TDto> ObterAsync<TDto>(
                Expression<Func<TEntity, bool>> expression,
                string? includes = null,
                string? orderBy = null,
                CancellationToken cancellationToken = default)
            {
                var set = _context.Set<TEntity>().AsQueryable();
                set = ApplyIncludes(set, includes);
                set = ApplyFilter(set, expression);
                set = ApplySort(set, orderBy);

                var result = await set.AsNoTracking().ProjectTo<TDto>(_mapperConfiguration).FirstOrDefaultAsync(cancellationToken);

                if (result == null)
                    throw new NotFoundException("Registro não encontrado no banco de dados.");

                return result;
            }

            public async Task<TDto> ObterAsync<TDto>(
                Expression<Func<TEntity, bool>> expression,
                Expression<Func<TEntity, TDto>> select,
                string? includes = null,
                string? orderBy = null,
                CancellationToken cancellationToken = default)
            {
                var set = _context.Set<TEntity>().AsQueryable();
                set = ApplyIncludes(set, includes);
                set = ApplyFilter(set, expression);
                set = ApplySort(set, orderBy);

                var result = await set.AsNoTracking().Select(select).FirstOrDefaultAsync(cancellationToken);

                if (result == null)
                    throw new NotFoundException("Registro não encontrado no banco de dados.");

                return result;
            }

            public async Task<TEntity> ObterAsync(Expression<Func<TEntity, bool>> expression, string? includes = null, string? orderBy = null, CancellationToken cancellationToken = default)
            {
                var set = _context.Set<TEntity>().AsQueryable();
                set = ApplyIncludes(set, includes);
                set = ApplyFilter(set, expression);
                set = ApplySort(set, orderBy);

                var result = await set.AsNoTracking().FirstOrDefaultAsync(cancellationToken);

                if (result == null)
                    throw new NotFoundException("Registro não encontrado no banco de dados.");

                return result;
            }

            #endregion

        }
    }
