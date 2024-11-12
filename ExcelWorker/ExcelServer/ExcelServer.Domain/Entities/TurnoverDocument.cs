using ExcelServer.Domain.Entities.Base;

namespace ExcelServer.Domain.Entities
{
    public class TurnoverDocument : FinancialEntity
    {
        public string BankName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Currency { get; set; } = null!;


        public IEnumerable<SummaryClass> SummaryClasses { get; set; } = null!;

    }
}