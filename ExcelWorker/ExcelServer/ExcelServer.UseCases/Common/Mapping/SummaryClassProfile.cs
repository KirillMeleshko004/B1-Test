using AutoMapper;
using ExcelServer.Domain.Entities;
using ExcelServer.UseCases.TurnoverDocuments.DTOs;

namespace ExcelServer.UseCases.Common.Mapping
{
    public class SummaryClassProfile : Profile
    {
        public SummaryClassProfile()
        {
            CreateMap<SummaryClass, SummaryClassDto>();
        }
    }
}