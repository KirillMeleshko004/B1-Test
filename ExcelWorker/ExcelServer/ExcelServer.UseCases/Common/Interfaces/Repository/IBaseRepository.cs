using System.Linq.Expressions;
using ExcelServer.UseCases.Common.Interfaces.Utility;
using ExcelServer.UseCases.Common.RequestFeatures;

namespace ExcelServer.UseCases.Common.Interfaces.Repository
{
   public interface IBaseRepository<T>
   {
      public Task<T?> GetSingle(Expression<Func<T, bool>> condition,
         Expression<Func<T, object>>? include = null,
         CancellationToken cancellationToken = default);
      public Task<IPagedEnumerable<T>> GetRange(RequestParameters parameters,
         Expression<Func<T, bool>>? condition = null,
         Expression<Func<T, object>>? include = null,
         CancellationToken cancellationToken = default);
      public void Create(T entity, CancellationToken cancellationToken);
      public void Update(T entity, CancellationToken cancellationToken);
      public void Delete(T entity, CancellationToken cancellationToken);
   }
}