using ExcelServer.Domain.Entities;
using ExcelServer.Infrastructure.DB.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExcelServer.Infrastructure.DB.Configuration
{
    public class AccountSummaryConfiguration : FinancialEntityConfiguration<AccountsSummary>
    {
        public override void Configure(EntityTypeBuilder<AccountsSummary> builder)
        {
            base.Configure(builder);

            builder.ToTable("AccountsSummary");

            builder.Property(accS => accS.Number)
                .IsRequired();

            builder.HasMany(accS => accS.Accounts)
                .WithOne()
                .HasForeignKey(a => a.SummaryId)
                .IsRequired();
        }
    }
}