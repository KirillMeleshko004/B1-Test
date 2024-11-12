using AutoMapper;
using ExcelServer.Domain.Entities;
using ExcelServer.UseCases.TurnoverDocuments.DTOs;

namespace ExcelServer.UseCases.Common.Mapping
{
    public class AccountsSummaryProfile : Profile
    {
        public AccountsSummaryProfile()
        {
            CreateMap<AccountsSummary, AccountsSummaryDto>();
        }
    }
}