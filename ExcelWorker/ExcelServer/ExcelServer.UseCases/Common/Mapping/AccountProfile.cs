using AutoMapper;
using ExcelServer.Domain.Entities;
using ExcelServer.UseCases.TurnoverDocuments.DTOs;

namespace ExcelServer.UseCases.Common.Mapping
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>();
        }
    }
}