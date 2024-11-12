using ExcelServer.UseCases.TurnoverDocuments.DTOs;
using MediatR;

namespace ExcelServer.UseCases.TurnoverDocuments.Commands
{
    public record UploadDocumentCommand(TurnoverDocumentUploadDto Document) : IRequest<TurnoverDocumentDto>;
}