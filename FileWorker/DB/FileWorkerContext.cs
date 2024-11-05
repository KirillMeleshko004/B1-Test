using FileWorker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileWorker.DB
{
    public class FileWorkerContext : DbContext
    {
        public DbSet<RecordModel> Records { get; set; }

        public FileWorkerContext(DbContextOptions<FileWorkerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}