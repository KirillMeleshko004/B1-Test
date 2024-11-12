using ExcelServer.Domain.Entities;
using ExcelServer.Infrastructure.DB.Common.Constants;
using ExcelServer.Infrastructure.DB.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExcelServer.Infrastructure.DB.Configuration
{
    public class TurnoverDocumentConfiguration : FinancialEntityConfiguration<TurnoverDocument>
    {
        public override void Configure(EntityTypeBuilder<TurnoverDocument> builder)
        {
            base.Configure(builder);

            builder.ToTable("TurnoverDocument");

            builder.Property(td => td.BankName)
                .IsRequired()
                .HasMaxLength(TurnoverDocumentConstants.BankNameMaxLength);

            builder.Property(td => td.Title)
                .IsRequired()
                .HasMaxLength(TurnoverDocumentConstants.TitleMaxLength);

            builder.Property(td => td.Date)
                .IsRequired();

            builder.Property(td => td.Currency)
                .IsRequired()
                .HasMaxLength(TurnoverDocumentConstants.CurrencyMaxLength);

            builder.HasMany(td => td.SummaryClasses)
                .WithOne()
                .HasForeignKey(sc => sc.TurnoverDocumentId)
                .IsRequired();
        }
    }
}