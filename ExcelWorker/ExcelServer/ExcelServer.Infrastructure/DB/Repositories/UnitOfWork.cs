using ExcelServer.Domain.Entities.Base;
using ExcelServer.Infrastructure.DB.Contexts;
using ExcelServer.UseCases.Common.Interfaces.Repository;

namespace ExcelServer.Infrastructure.DB.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExcelWorkerDbContext _context;
        private Lazy<ITurnoverDocumentRepository> _turnoverDocuments;
        public ITurnoverDocumentRepository TurnoverDocuments { get { return _turnoverDocuments.Value; } }

        public UnitOfWork(ExcelWorkerDbContext context)
        {
            _context = context;
            _turnoverDocuments = new Lazy<ITurnoverDocumentRepository>(() =>
            {
                return new TurnoverDocumentRepository(context); 
            });
        }

        public async Task SaveChangesAsync()
        {
            _context.ChangeTracker.DetectChanges();
            var changed = _context.ChangeTracker.Entries()
               .Where(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Modified ||
                  e.State == Microsoft.EntityFrameworkCore.EntityState.Added);

            foreach (var entry in changed)
            {
                if (!typeof(BaseEntity).IsAssignableFrom(entry.Entity.GetType()))
                {
                    continue;
                }
                var entity = entry.Entity as BaseEntity;

                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                {
                    entity!.CreatedAt = DateTime.Now;
                }

                entity!.ModifiedAt = DateTime.Now;
            }
            await _context.SaveChangesAsync();
        }
    }
}