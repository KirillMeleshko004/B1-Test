namespace ExcelServer.UseCases.TurnoverDocuments.DTOs
{
    public record TurnoverDocumentDto
    {
        public Guid Id { get; set; }
        public string BankName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Currency { get; set; } = null!;
        public DateTime CreationDate { get; set; }
    }
}