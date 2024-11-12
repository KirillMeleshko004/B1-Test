namespace ExcelServer.UseCases.TurnoverDocuments.DTOs
{
    public record TurnoverDocumentUploadDto
    {
        public Stream DocumentName { get; set; } = null!;
        public byte[] ExcelDocument { get; set; } = null!;
    }
}