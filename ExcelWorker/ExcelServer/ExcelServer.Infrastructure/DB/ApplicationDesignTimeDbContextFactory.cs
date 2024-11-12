using ExcelServer.Infrastructure.DB.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExcelServer.Infrastructure.DB
{
    public class ApplicationDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExcelWorkerDbContext>
    {
        /// <summary>
        /// args: SQL Server connection string
        /// </summary>
        public ExcelWorkerDbContext CreateDbContext(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("Connection string is not provided");
            }

            var conncentionString = args[0];
            var optionsBuilder = new DbContextOptionsBuilder<ExcelWorkerDbContext>();

            optionsBuilder.UseSqlServer(conncentionString,
                x =>
                {
                    x.MigrationsAssembly(typeof(ExcelWorkerDbContext).Assembly.FullName);
                });

            return new ExcelWorkerDbContext(optionsBuilder.Options);
        }
    }
}