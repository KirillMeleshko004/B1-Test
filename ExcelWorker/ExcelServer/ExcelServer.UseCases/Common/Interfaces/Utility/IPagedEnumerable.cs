using ExcelServer.UseCases.Common.Utility;

namespace ExcelServer.UseCases.Common.Interfaces.Utility
{
    public interface IPagedEnumerable<T> : IEnumerable<T>
    {
        public IEnumerable<T> Items { get; }
        public PageMetaData Pages { get; }
    }
}