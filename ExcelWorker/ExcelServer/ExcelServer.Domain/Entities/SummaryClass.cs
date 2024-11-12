using ExcelServer.Domain.Entities.Base;

namespace ExcelServer.Domain.Entities
{
    public class SummaryClass : FinancialEntity
    {
        public int Number { get; set; }
        public string Title { get; set; } = null!;

        //List of summarized information about group of accounts
        public IEnumerable<AccountsSummary> AccountSummaries { get; set; } = null!;

        public Guid TurnoverDocumentId { get; set; }
    }
}