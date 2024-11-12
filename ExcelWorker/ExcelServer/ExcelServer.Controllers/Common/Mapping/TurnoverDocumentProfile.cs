using AutoMapper;
using ExcelServer.Controllers.ViewModels;
using ExcelServer.UseCases.TurnoverDocuments.DTOs;
using Microsoft.AspNetCore.Http;

namespace ExcelServer.Controllers.Common.Mapping
{
    public class TurnoverDocumentProfile : Profile
    {
        public TurnoverDocumentProfile()
        {
            CreateMap<IFormFile, byte[]>()
                .ConvertUsing<IFormFileTypeConverter>();

            CreateMap<TurnoverDocumentUploadViewModel, TurnoverDocumentUploadDto>()
                .ForMember(dto => dto.DocumentName, options =>
                {
                    options.MapFrom(vm => Path.GetFileName(vm.Document.FileName));
                });
        }

        public class IFormFileTypeConverter : ITypeConverter<IFormFile, byte[]>
        {
            public byte[] Convert(IFormFile source, byte[] destination, ResolutionContext context)
            {
                using var stream = new MemoryStream();

                source.CopyTo(stream);

                return stream.ToArray();
            }
        }
    }
}