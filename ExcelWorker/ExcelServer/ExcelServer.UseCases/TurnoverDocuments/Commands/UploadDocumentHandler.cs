using AutoMapper;
using ExcelServer.Domain.Entities;
using ExcelServer.UseCases.Common.Exceptions;
using ExcelServer.UseCases.Common.Interfaces.Repository;
using ExcelServer.UseCases.TurnoverDocuments.DTOs;
using MediatR;
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

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

        public async Task<TurnoverDocumentDto> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var ms = new MemoryStream(request.Document.ExcelDocument))
                {
                    HSSFWorkbook book = new HSSFWorkbook(ms);
                    var sheet = book.GetSheetAt(0);

                    var td = ExtractGeneralInformation(sheet);

                    ParseExcelDocument(sheet, ref td);

                    _unitOfWork.TurnoverDocuments.Create(td, cancellationToken);

                    await _unitOfWork.SaveChangesAsync();

                    _logger.LogInformation("Turnover document created");

                    var tdDto = _mapper.Map<TurnoverDocumentDto>(td);

                    return tdDto;
                }

            }
            catch
            {
                throw;
            }
        }

        private TurnoverDocument ExtractGeneralInformation(ISheet sheet)
        {
            var td = new TurnoverDocument
            {
                BankName = sheet.GetRow(0).GetCell(0).ToString(),

                Title = $@"{sheet.GetRow(1).GetCell(0)} {sheet.GetRow(2).GetCell(0)} {sheet.GetRow(3).GetCell(0)}",

                Date = DateTime.Parse(sheet.GetRow(5).GetCell(0).ToString()),

                Currency = sheet.GetRow(5).GetCell(6).ToString(),

                SummaryClasses = []
            };

            return td;
        }

        private void ParseExcelDocument(ISheet sheet, ref TurnoverDocument td)
        {
            List<SummaryClass> summaryClasses = [];
            List<AccountsSummary> accountsSummaries = [];
            List<Account> accounts = [];

            for (int i = 8; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);

                if (TryParseSummaryClassTitle(row, out string title))
                {
                    var sc = new SummaryClass()
                    {
                        Title = title,
                        Number = summaryClasses.Count + 1,
                        AccountSummaries = []
                    };
                    summaryClasses.Add(sc);
                    continue;
                }

                if (TryParseSummaryClass(row, out var csValues))
                {
                    if (summaryClasses.Count == 0)
                    {
                        throw new InvalidExcelFormatException(i);
                    }

                    summaryClasses[^1].OpeningBalanceAsset = csValues[0];
                    summaryClasses[^1].OpeningBalanceLiability = csValues[1];
                    summaryClasses[^1].TurnoverDebit = csValues[2];
                    summaryClasses[^1].TurnoverCredit = csValues[3];
                    summaryClasses[^1].ClosingBalanceAsset = csValues[4];
                    summaryClasses[^1].ClosingBalanceLiability = csValues[5];
                    summaryClasses[^1].AccountSummaries = accountsSummaries;

                    accountsSummaries = [];
                    accounts = [];

                    continue;
                }

                if (TryParseAccountsSummary(row, out var accSValues, out var accSNumber))
                {
                    if (summaryClasses.Count == 0)
                    {
                        throw new InvalidExcelFormatException(i);
                    }

                    var accSummary = new AccountsSummary()
                    {
                        OpeningBalanceAsset = accSValues[0],
                        OpeningBalanceLiability = accSValues[1],
                        TurnoverDebit = accSValues[2],
                        TurnoverCredit = accSValues[3],
                        ClosingBalanceAsset = accSValues[4],
                        ClosingBalanceLiability = accSValues[5],
                        Number = accSNumber,
                        Accounts = accounts
                    };

                    accountsSummaries.Add(accSummary);
                    accounts = [];

                    continue;
                }


                if (TryParseAccount(row, out var aValues, out var aNumber))
                {
                    var account = new Account()
                    {
                        Number = aNumber,
                        OpeningBalanceAsset = aValues[0],
                        OpeningBalanceLiability = aValues[1],
                        TurnoverDebit = aValues[2],
                        TurnoverCredit = aValues[3],
                        ClosingBalanceAsset = aValues[4],
                        ClosingBalanceLiability = aValues[5]
                    };

                    accounts.Add(account);
                    continue;
                }

                if (TryParseTurnoverDocument(row, out var tdValues))
                {
                    td.OpeningBalanceAsset = tdValues[0];
                    td.OpeningBalanceLiability = tdValues[1];
                    td.TurnoverDebit = tdValues[2];
                    td.TurnoverCredit = tdValues[3];
                    td.ClosingBalanceAsset = tdValues[4];
                    td.ClosingBalanceLiability = tdValues[5];

                    td.SummaryClasses = summaryClasses;
                    return;
                }

                throw new InvalidExcelFormatException(i);

            }
        }

        private static bool TryParseSummaryClassTitle(IRow row, out string title)
        {
            var first = row.GetCell(0).ToString();

            if (!first.StartsWith("КЛАСС"))
            {
                title = "";
                return false;
            }

            title = first;
            return true;
        }

        private static bool TryParseSummaryClass(IRow row, out decimal[] values)
        {
            var first = row.GetCell(0).ToString();

            if (!first.StartsWith("ПО КЛАССУ"))
            {
                values = [];
                return false;
            }

            values = new decimal[6];

            for (int i = 0; i < 6; i++)
            {
                values[i] = decimal.Parse(row.GetCell(i + 1).ToString());
            }

            return true;
        }

        private static bool TryParseAccountsSummary(IRow row, out decimal[] values, out int number)
        {
            var first = row.GetCell(0).ToString();

            if (!int.TryParse(first, out number))
            {
                values = [];
                number = 0;
                return false;
            }

            if (number >= 1000)
            {
                values = [];
                return false;
            }

            values = new decimal[6];

            for (int i = 0; i < 6; i++)
            {
                values[i] = decimal.Parse(row.GetCell(i + 1).ToString());
            }

            return true;

        }

        private static bool TryParseTurnoverDocument(IRow row, out decimal[] values)
        {
            var first = row.GetCell(0).ToString();

            if (!first.StartsWith("БАЛАНС"))
            {
                values = [];
                return false;
            }

            values = new decimal[6];

            for (int i = 0; i < 6; i++)
            {
                values[i] = decimal.Parse(row.GetCell(i + 1).ToString());
            }

            return true;
        }

        private static bool TryParseAccount(IRow row, out decimal[] values, out int number)
        {
            var first = row.GetCell(0).ToString();

            if (!int.TryParse(first, out number))
            {
                values = [];
                number = 0;
                return false;
            }

            values = new decimal[6];

            for (int i = 0; i < 6; i++)
            {
                values[i] = decimal.Parse(row.GetCell(i + 1).ToString());
            }

            return true;
        }


    }


}