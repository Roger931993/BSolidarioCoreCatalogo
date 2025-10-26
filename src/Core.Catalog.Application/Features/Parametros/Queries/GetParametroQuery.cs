using Core.Catalog.Application.DTOs;
using Core.Catalog.Application.DTOs.Base;
using MediatR;

namespace Core.Catalog.Application.Features.Parametros.Queries
{
    public record class GetParametroQuery(RequestBase<GetParametroRequest> request) : IRequest<ResponseBase<GetParametroResponse>>;
    public class GetParametroRequest
    {
        public int parametro_id { get; set; }        
    }

    public class GetParametroResponse
    {
        public parametroDto? parametro { get; set; }
    }
}
