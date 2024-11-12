using System.Linq.Expressions;
using ExcelServer.Domain.Entities.Base;
using ExcelServer.Infrastructure.DB.Contexts;
using ExcelServer.Infrastructure.DB.Extensions;
using ExcelServer.UseCases.Common.Interfaces.Repository;
using ExcelServer.UseCases.Common.Interfaces.Utility;
using ExcelServer.UseCases.Common.RequestFeatures;
using ExcelServer.UseCases.Common.Utility;
using Microsoft.EntityFrameworkCore;

namespace ExcelServer.Infrastructure.DB.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private protected readonly ExcelWorkerDbContext _context;

        public BaseRepository(ExcelWorkerDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetSingle(Expression<Func<T, bool>> condition,
           Expression<Func<T, object>>? include = null,
           CancellationToken cancellationToken = default)
        {
            var query = _context.Set<T>()
               .Where(condition)
               .AsNoTracking();

            if (include != null)
            {
                query = query.Include(include);
            }

            return await query
               .SingleOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<IPagedEnumerable<T>> GetRange(RequestParameters parameters,
           Expression<Func<T, bool>>? condition,
           Expression<Func<T, object>>? include = null,
           CancellationToken cancellationToken = default)
        {
            var query = _context.Set<T>()
               .AsNoTracking();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            var count = query.Count();

            query = query
               .Page(parameters);

            if (include != null)
            {
                query = query.Include(include);
            }

            var result = await query
               .AsNoTracking()
               .ToListAsync(cancellationToken);

            return new PagedList<T>(result, parameters.PageSize,
               parameters.PageNumber, count);
        }

        public void Create(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Remove(entity);
        }

    }
}