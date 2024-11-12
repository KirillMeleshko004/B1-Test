using AutoMapper;
using ExcelServer.UseCases.Common.Interfaces.Repository;
using ExcelServer.UseCases.Common.Interfaces.Utility;
using ExcelServer.UseCases.TurnoverDocuments.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ExcelServer.UseCases.TurnoverDocuments.Queries
{
    public class ListDocumentsHandler : IRequestHandler<ListDocumentsQuery, IPagedEnumerable<TurnoverDocumentDto>>
    {
        private readonly ILogger<ListDocumentsHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListDocumentsHandler(ILogger<ListDocumentsHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IPagedEnumerable<TurnoverDocumentDto>> Handle(ListDocumentsQuery request, CancellationToken cancellationToken)
        {
            var tds = await _unitOfWork.TurnoverDocuments.GetRange(request.Parameters, cancellationToken: cancellationToken);

            var dtos = _mapper.Map<IPagedEnumerable<TurnoverDocumentDto>>(tds);

            return dtos;
        }
    }
}