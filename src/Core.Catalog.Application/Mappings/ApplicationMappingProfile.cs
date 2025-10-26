#region References
using AutoMapper;
using Core.Catalog.Application.DTOs;
using Core.Catalog.Application.Features.CatalogError.Commands;
using Core.Catalog.Domain.Entities;
#endregion


namespace Core.Catalog.Application.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<catalog_error, RegisterErrorResponse>();           
            CreateMap<catalog_error, catalog_errorDto>();           
            CreateMap<parametros, parametroDto>();           
        }
    }
}
