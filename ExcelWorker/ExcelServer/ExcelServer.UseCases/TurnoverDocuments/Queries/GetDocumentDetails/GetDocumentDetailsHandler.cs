using ExcelServer.UseCases.TurnoverDocuments.DTOs;
using MediatR;

namespace ExcelServer.UseCases.TurnoverDocuments.Queries
{
    public class GetDocumentDetailsHandler : IRequestHandler<GetDocumentDetailsQuery, TurnoverDocumentDetailsDto>
    {
        public Task<TurnoverDocumentDetailsDto> Handle(GetDocumentDetailsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}