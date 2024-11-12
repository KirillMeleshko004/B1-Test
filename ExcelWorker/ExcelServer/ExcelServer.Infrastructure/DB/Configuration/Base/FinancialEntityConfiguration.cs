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
                .HasColumnType("MONEY");
            builder.Property(fe => fe.OpeningBalanceLiability)
                .IsRequired()
                .HasColumnType("MONEY");

            builder.Property(fe => fe.TurnoverDebit)
                .IsRequired()
                .HasColumnType("MONEY");
            builder.Property(fe => fe.TurnoverCredit)
                .IsRequired()
                .HasColumnType("MONEY");

            builder.Property(fe => fe.ClosingBalanceAsset)
                .IsRequired()
                .HasColumnType("MONEY");
            builder.Property(fe => fe.ClosingBalanceLiability)
                .IsRequired()
                .HasColumnType("MONEY");
        }
    }
}