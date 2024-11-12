namespace ExcelServer.UseCases.Common.RequestFeatures
{
    public record RequestParameters
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}