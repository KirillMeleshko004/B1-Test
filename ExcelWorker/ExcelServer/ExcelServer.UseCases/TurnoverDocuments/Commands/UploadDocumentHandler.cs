using AutoMapper;
using ExcelServer.UseCases.Common.Interfaces.Repository;
using ExcelServer.UseCases.TurnoverDocuments.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ExcelServer.UseCases.TurnoverDocuments.Commands
{
    public class UploadDocumentHandler : IRequestHandler<UploadDocumentCommand, TurnoverDocumentDto>
    {
        private readonly ILogger<UploadDocumentHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UploadDocumentHandler(ILogger<UploadDocumentHandler> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<TurnoverDocumentDto> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var ms = new MemoryStream(request.Document.ExcelDocument))
                {
                    HSSFWorkbook book = new HSSFWorkbook(ms);
                    var sheet = book.GetSheetAt(0);

                    for (int i = 0; i <= sheet.LastRowNum; i++)
                    {
                        foreach (var cell in sheet.GetRow(i))
                        {
                            System.Console.WriteLine(cell.StringCellValue);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            throw new NotImplementedException();
        }
    }


}