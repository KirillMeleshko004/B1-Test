using Microsoft.AspNetCore.Http;

namespace ExcelServer.Controllers.ViewModels
{
    public record TurnoverDocumentUploadViewModel
    {
        public IFormFile ExcelDocument { get; set; } = null!;
    }
}