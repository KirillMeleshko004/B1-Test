using AutoMapper;
using ExcelServer.Domain.Entities;
using ExcelServer.UseCases.TurnoverDocuments.DTOs;

namespace ExcelServer.UseCases.Common.Mapping
{
    public class TurnoverDocumentProfile : Profile
    {
        public TurnoverDocumentProfile()
        {
            CreateMap<TurnoverDocument, TurnoverDocumentDto>()
                .ForMember(dto => dto.CreationDate, options =>
                {
                    options.MapFrom(e => e.CreatedAt);
                });

            CreateMap<TurnoverDocument, TurnoverDocumentDetailsDto>()
                .ForMember(dto => dto.CreationDate, options =>
                {
                    options.MapFrom(e => e.CreatedAt);
                });
        }
    }
}