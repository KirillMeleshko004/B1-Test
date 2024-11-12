using ExcelServer.Domain.Entities;
using ExcelServer.Infrastructure.DB.Configuration.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExcelServer.Infrastructure.DB.Configuration
{
    public class AccountConfiguration : FinancialEntityConfiguration<Account>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder);

            builder.ToTable("Account");

            builder.Property(a => a.Number)
                .IsRequired();
        }
    }
}