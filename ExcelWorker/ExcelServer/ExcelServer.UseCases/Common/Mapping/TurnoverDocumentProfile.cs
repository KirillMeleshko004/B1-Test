using AutoMapper;
using ExcelServer.Domain.Entities;
using ExcelServer.UseCases.TurnoverDocuments.DTOs;

namespace ExcelServer.UseCases.Common.Mapping
{
    public class TurnoverDocumentProfile : Profile
    {
        public TurnoverDocumentProfile()
        {
            CreateMap<TurnoverDocument, TurnoverDocumentDto>();

            CreateMap<TurnoverDocument, TurnoverDocumentDetailsDto>();
        }
    }
}