using Core.Catalog.Application.DTOs;
using Core.Catalog.Application.DTOs.Base;
using MediatR;

namespace Core.Catalog.Application.Features.CatalogError.Commands
{
    public class RegisterErrorCommand : RequestBase<RegisterErrorRequest>, IRequest<ResponseBase<RegisterErrorResponse>>
    {
    }

    public class RegisterErrorRequest
    {
        public int? catalog_error_id { get; set; }
        public string? error_description { get; set; }
        public string? error_priority { get; set; }
        public int? error_status_code { get; set; }
    }

    public class RegisterErrorResponse
    {
        public catalog_errorDto? catalog_error { get; set; }
    }
}
