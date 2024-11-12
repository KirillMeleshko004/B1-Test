using ExcelServer.UseCases.Common.Interfaces.Utility;
using ExcelServer.UseCases.Common.RequestFeatures;
using ExcelServer.UseCases.TurnoverDocuments.DTOs;
using MediatR;

namespace ExcelServer.UseCases.TurnoverDocuments.Queries
{
    public record ListDocumentsQuery(RequestParameters Parameters) : IRequest<IPagedEnumerable<TurnoverDocumentDto>>;
}