namespace ExcelServer.UseCases.Common.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        ITurnoverDocumentRepository TurnoverDocuments { get; }

        Task SaveChangesAsync();
    }
}