namespace ExcelServer.Domain.Entities.Base
{
    /// <summary>
    /// Base class for entities with attached financial data
    /// </summary>
    public abstract class FinancialEntity : BaseEntity
    {
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