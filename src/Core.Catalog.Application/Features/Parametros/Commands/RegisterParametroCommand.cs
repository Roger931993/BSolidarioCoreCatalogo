using Core.Catalog.Application.DTOs;
using Core.Catalog.Application.DTOs.Base;
using MediatR;

namespace Core.Catalog.Application.Features.Parametros.Commands
{
    public class RegisterParametroCommand : RequestBase<RegisterParametroRequest>, IRequest<ResponseBase<RegisterParametroResponse>>
    {
    }

    public class RegisterParametroRequest
    {
        public int? parametros_id { get; set; }
        public string? nombre_parametro { get; set; }
        public string? valor { get; set; }
    }

    public class RegisterParametroResponse
    {
        public parametroDto? parametro { get; set; }
    }
}
