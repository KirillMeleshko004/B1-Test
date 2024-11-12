using ExcelServer.UseCases.Common.RequestFeatures;

namespace ExcelServer.Infrastructure.DB.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> entities,
           RequestParameters parameters)
        {
            return entities
               .Skip((parameters.PageNumber - 1) * parameters.PageSize)
               .Take(parameters.PageSize);
        }
    }
}