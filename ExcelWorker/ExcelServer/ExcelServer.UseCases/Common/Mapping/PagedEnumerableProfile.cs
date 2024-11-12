using AutoMapper;
using ExcelServer.UseCases.Common.Interfaces.Utility;
using ExcelServer.UseCases.Common.Utility;

namespace ExcelServer.UseCases.Common.Mapping
{
    public class PagedEnumerableProfile : Profile
    {
        public PagedEnumerableProfile()
        {
            CreateMap(typeof(IPagedEnumerable<>), typeof(IPagedEnumerable<>))
                .ConvertUsing(typeof(PagedEnumerableConverter<,>));
        }

        class PagedEnumerableConverter<TSource, TDest> :
            ITypeConverter<IPagedEnumerable<TSource>, IPagedEnumerable<TDest>>
        {
            public IPagedEnumerable<TDest> Convert(IPagedEnumerable<TSource> source,
                IPagedEnumerable<TDest> destination, ResolutionContext context)
            {
                var itmes = source.Items;
                var metadata = source.Pages;

                var destItems = context.Mapper.Map<IEnumerable<TDest>>(itmes);

                return new PagedList<TDest>(destItems, metadata);
            }
        }
    }
}