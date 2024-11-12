using System.Linq.Expressions;
using ExcelServer.Domain.Entities;
using ExcelServer.Infrastructure.DB.Contexts;
using ExcelServer.Infrastructure.DB.Extensions;
using ExcelServer.UseCases.Common.Interfaces.Repository;
using ExcelServer.UseCases.Common.Interfaces.Utility;
using ExcelServer.UseCases.Common.RequestFeatures;
using ExcelServer.UseCases.Common.Utility;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace ExcelServer.Infrastructure.DB.Repositories
{
    public class TurnoverDocumentRepository : BaseRepository<TurnoverDocument>, ITurnoverDocumentRepository
    {
        public TurnoverDocumentRepository(ExcelWorkerDbContext context) : base(context)
        {

        }

        public async Task<TurnoverDocument?> GetDetailedTurnoverDocument(Expression<Func<TurnoverDocument, bool>> condition, CancellationToken cancellationToken = default)
        {

            var query = _context.Set<TurnoverDocument>()
               .Where(condition)
               .Include(td => td.SummaryClasses)
               .ThenInclude(sc => sc.AccountSummaries)
               .ThenInclude(accS => accS.Accounts)
               .AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }

        public override async Task<IPagedEnumerable<TurnoverDocument>> GetRange(RequestParameters parameters,
           Expression<Func<TurnoverDocument, bool>>? condition,
           Expression<Func<TurnoverDocument, object>>? include = null,
           CancellationToken cancellationToken = default)
        {
            var query = _context.Set<TurnoverDocument>()
                .OrderByDescending(td => td.CreatedAt)
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
                .ToListAsync(cancellationToken);

            return new PagedList<TurnoverDocument>(result, parameters.PageSize,
               parameters.PageNumber, count);
        }
    }
}