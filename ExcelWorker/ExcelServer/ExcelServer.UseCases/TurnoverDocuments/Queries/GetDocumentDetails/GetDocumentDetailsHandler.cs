using AutoMapper;
using ExcelServer.UseCases.Common.Interfaces.Repository;
using ExcelServer.UseCases.TurnoverDocuments.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ExcelServer.UseCases.TurnoverDocuments.Queries
{
    public class GetDocumentDetailsHandler : IRequestHandler<GetDocumentDetailsQuery, TurnoverDocumentDetailsDto>
    {
        private readonly ILogger<GetDocumentDetailsHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDocumentDetailsHandler(ILogger<GetDocumentDetailsHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TurnoverDocumentDetailsDto> Handle(GetDocumentDetailsQuery request, CancellationToken cancellationToken)
        {
            var td = await _unitOfWork.TurnoverDocuments.GetDetailedTurnoverDocument(td => td.Id.Equals(request.Id), cancellationToken);

            var dto = _mapper.Map<TurnoverDocumentDetailsDto>(td);

            return dto;
        }
    }
}