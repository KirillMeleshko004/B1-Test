namespace ExcelServer.UseCases.TurnoverDocuments.DTOs
{
    public record TurnoverDocumentDetailsDto
    {
        public Guid Id { get; set; }
        public string BankName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime CreationDate { get; set; }

        public IEnumerable<SummaryClassDto> SummaryClasses { get; set; } = null!;

        #region Financial values

        #region Opening Balance

        public decimal OpeningBalanceAsset { get; set; }
        public decimal OpeningBalanceLiability { get; set; }

        #endregion

        #region Turnover

        public decimal TurnoverDebit { get; set; }
        public decimal TurnoverCredit { get; set; }

        #endregion

        #region Closing Balance

        public decimal ClosingBalanceAsset { get; set; }
        public decimal ClosingBalanceLiability { get; set; }

        #endregion

        #endregion

    }
}