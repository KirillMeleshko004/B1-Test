using System.Reflection;
using ExcelServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExcelServer.Infrastructure.DB.Contexts
{
    public class ExcelWorkerDbContext : DbContext
    {
        public DbSet<TurnoverDocument> Documents { get; set; }
        public DbSet<SummaryClass> SummaryClasses { get; set; }
        public DbSet<AccountsSummary> AccountsSummaries { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public ExcelWorkerDbContext(DbContextOptions<ExcelWorkerDbContext> options)
         : base(options)
        {
            if (Database.IsSqlServer() && Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

    }
}