using AutoMapper;
using ExcelServer.Controllers.ViewModels;
using ExcelServer.UseCases.TurnoverDocuments.Commands;
using ExcelServer.UseCases.TurnoverDocuments.DTOs;
using ExcelServer.UseCases.TurnoverDocuments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExcelServer.Controllers
{
    [ApiController]
    [Route("api/turnover-documents")]
    public class TurnoverDocumentsController : ControllerBase
    {
        private readonly ILogger<TurnoverDocumentsController> _logger;
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public TurnoverDocumentsController(ILogger<TurnoverDocumentsController> logger, ISender sender, IMapper mapper)
        {
            _logger = logger;
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}/details")]
        public async Task<IActionResult> GetDetails(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetDocumentDetailsQuery(id);

            var result = await _sender.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] TurnoverDocumentUploadViewModel vm, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TurnoverDocumentUploadDto>(vm);

            var uploadCommand = new UploadDocumentCommand(dto);

            var result = await _sender.Send(uploadCommand, cancellationToken);

            return CreatedAtAction(nameof(GetDetails), new { id = result.Id }, result);
        }
    }
}