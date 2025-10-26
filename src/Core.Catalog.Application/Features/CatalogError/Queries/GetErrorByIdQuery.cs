using Core.Catalog.Application.DTOs;
using Core.Catalog.Application.DTOs.Base;
using MediatR;

namespace Core.Catalog.Application.Features.CatalogError.Queries
{
    public record class GetErrorByIdQuery(RequestBase<GetErrorByIdRequest> request) : IRequest<ResponseBase<GetErrorByIdResponse>>;
    public class GetErrorByIdRequest
    {
        public int catalog_error_id { get; set; }        
    }

    public class GetErrorByIdResponse
    {
        public catalog_errorDto? catalog_error { get; set; }
    }
}
