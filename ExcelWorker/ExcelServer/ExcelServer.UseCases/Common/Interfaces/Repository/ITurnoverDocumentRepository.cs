using System.Linq.Expressions;
using ExcelServer.Domain.Entities;

namespace ExcelServer.UseCases.Common.Interfaces.Repository
{
    public interface ITurnoverDocumentRepository : IBaseRepository<TurnoverDocument>
    {
        public Task<TurnoverDocument?> GetDetailedTurnoverDocument(Expression<Func<TurnoverDocument, bool>> condition,
           CancellationToken cancellationToken = default);
    }
}