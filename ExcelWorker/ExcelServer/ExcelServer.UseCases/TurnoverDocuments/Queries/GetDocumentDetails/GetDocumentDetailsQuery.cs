using ExcelServer.UseCases.TurnoverDocuments.DTOs;
using MediatR;

namespace ExcelServer.UseCases.TurnoverDocuments.Queries
{
    public record GetDocumentDetailsQuery(Guid Id) : IRequest<TurnoverDocumentDetailsDto>;
}