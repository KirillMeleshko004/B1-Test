using ExcelServer.Domain.Entities.Base;

namespace ExcelServer.Domain.Entities
{
    public class Account : FinancialEntity
    {
        public int Number { get; set; }

        public Guid SummaryId { get; set; }
    }
}