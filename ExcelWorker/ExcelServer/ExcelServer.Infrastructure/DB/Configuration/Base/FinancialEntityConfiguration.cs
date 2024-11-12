using ExcelServer.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExcelServer.Infrastructure.DB.Configuration.Base
{
    public abstract class FinancialEntityConfiguration<T> : BaseEntityConfiguration<T> where T : FinancialEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(fe => fe.OpeningBalanceAsset)
                .IsRequired()
                .HasPrecision(30, 2);
            builder.Property(fe => fe.OpeningBalanceLiability)
                .IsRequired()
                .HasPrecision(30, 2);

            builder.Property(fe => fe.TurnoverDebit)
                .IsRequired()
                .HasPrecision(30, 2);
            builder.Property(fe => fe.TurnoverCredit)
                .IsRequired()
                .HasPrecision(30, 2);

            builder.Property(fe => fe.ClosingBalanceAsset)
                .IsRequired()
                .HasPrecision(30, 2);
            builder.Property(fe => fe.ClosingBalanceLiability)
                .IsRequired()
                .HasPrecision(30, 2);
        }
    }
}