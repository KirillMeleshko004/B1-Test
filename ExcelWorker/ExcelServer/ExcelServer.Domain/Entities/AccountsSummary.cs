using ExcelServer.Domain.Entities.Base;

namespace ExcelServer.Domain.Entities
{
    public class AccountsSummary : FinancialEntity
    {
        public int Number { get; set; }

        public IEnumerable<Account> Accounts { get; set; } = null!;
        public Guid SummaryClassId { get; set; }
    }
}