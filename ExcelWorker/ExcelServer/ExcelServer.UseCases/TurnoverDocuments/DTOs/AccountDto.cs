namespace ExcelServer.UseCases.TurnoverDocuments.DTOs
{
    public record AccountDto
    {
        public int Number { get; set; }

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

    }
}