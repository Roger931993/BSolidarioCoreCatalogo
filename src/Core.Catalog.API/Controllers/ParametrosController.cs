using Core.Catalog.API.Filters;
using Core.Catalog.Application.DTOs.Base;
using Core.Catalog.Application.Features.Parametros.Commands;
using Core.Catalog.Application.Features.Parametros.Queries;
using Core.Catalog.Application.Interfaces.Infraestructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Core.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParametrosController : CommonController
    {
        public ParametrosController(IMediator mediator, IMemoryCacheLocalService memoryCacheLocalService, IRedisCache redisCache) : base(mediator, memoryCacheLocalService, redisCache)
        {
        }

        //[NOMBRE_CONTROLADOR]-[NOMBRE-METODO]
        [HttpGet("parametro/{id}")]
        [Permission("ParametrosController-errorbyid")]
        [ProducesResponseType(typeof(GetParametroResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetParametroResponse>> parametro(int id)
        {
            RequestBase<GetParametroRequest> request = new RequestBase<GetParametroRequest>()
            {
                Request = new GetParametroRequest()
                {
                    parametro_id = id
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<GetParametroResponse> objResponse = await _mediator.Send(new GetParametroQuery(request));
            return OkUrban(objResponse);
        }

        [HttpPost("register")]
        [Authorize]
        [Permission("ParametrosController-Register")]
        [ProducesResponseType(typeof(RegisterParametroResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RegisterParametroResponse>> Register([FromBody] RegisterParametroRequest user)
        {
            RegisterParametroCommand command = new RegisterParametroCommand()
            {
                Request = user
            };
            await CreateDataCacheLocal(HttpContext, command);
            ResponseBase<RegisterParametroResponse> objResponse = await _mediator.Send(command);
            return OkUrban(objResponse);
        }

        [HttpPost("load-cache")]
        [Permission("ParametrosController-LoadCache")]
        [ProducesResponseType(typeof(LoadParametroCacheResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<LoadParametroCacheResponse>> LoadCache()
        {
            LoadParametroCacheCommand command = new LoadParametroCacheCommand();
            await CreateDataCacheLocal(HttpContext, command);
            ResponseBase<LoadParametroCacheResponse> objResponse = await _mediator.Send(command);
            return OkUrban(objResponse);
        }
    }
}
