using ExcelServer.Domain.Entities;
using ExcelServer.Infrastructure.DB.Common.Constants;
using ExcelServer.Infrastructure.DB.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExcelServer.Infrastructure.DB.Configuration
{
    public class SummaryClassConfiguration : FinancialEntityConfiguration<SummaryClass>
    {
        public override void Configure(EntityTypeBuilder<SummaryClass> builder)
        {
            base.Configure(builder);

            builder.ToTable("SummaryClass");

            builder.Property(sc => sc.Number)
                .IsRequired();

            builder.Property(sc => sc.Title)
                .IsRequired()
                .HasMaxLength(SummaryClassConstants.TitleMaxLength);

            builder.HasMany(sc => sc.AccountSummaries)
                .WithOne()
                .HasForeignKey(accS => accS.SummaryClassId)
                .IsRequired();
        }
    }
}