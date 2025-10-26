using Core.Catalog.API.Filters;
using Core.Catalog.Application.DTOs.Base;
using Core.Catalog.Application.Features.CatalogError.Commands;
using Core.Catalog.Application.Features.CatalogError.Queries;
using Core.Catalog.Application.Interfaces.Infraestructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Core.Catalog.API.Controllers
{
    [ApiController]    
    [Route("api/[controller]")]
    public class CatalogErrorController : CommonController
    {
        public CatalogErrorController(IMediator mediator, IMemoryCacheLocalService memoryCacheLocalService, IRedisCache redisCache) : base(mediator, memoryCacheLocalService, redisCache)
        {
        }

        //[NOMBRE_CONTROLADOR]-[NOMBRE-METODO]
        [HttpGet("code-error/{id}")]
        [Permission("CatalogErrorController-errorbyid")]
        [ProducesResponseType(typeof(GetErrorByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetErrorByIdResponse>> errorbyid(int id)
        {
            RequestBase<GetErrorByIdRequest> request = new RequestBase<GetErrorByIdRequest>()
            {
                Request = new GetErrorByIdRequest()
                {
                    catalog_error_id = id
                }
            };
            await CreateDataCacheLocal(HttpContext, request);
            ResponseBase<GetErrorByIdResponse> objResponse = await _mediator.Send(new GetErrorByIdQuery(request));
            return OkUrban(objResponse);
        }

        [HttpPost("register")]
        [Authorize]
        [Permission("CatalogErrorController-Register")]
        [ProducesResponseType(typeof(RegisterErrorResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RegisterErrorResponse>> Register([FromBody] RegisterErrorRequest user)
        {
            RegisterErrorCommand command = new RegisterErrorCommand()
            {
                Request = user
            };
            await CreateDataCacheLocal(HttpContext, command);
            ResponseBase<RegisterErrorResponse> objResponse = await _mediator.Send(command);
            return OkUrban(objResponse);
        }

        [HttpPost("load-cache")]        
        [Permission("CatalogErrorController-LoadCache")]
        [ProducesResponseType(typeof(LoadCacheResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<LoadCacheResponse>> LoadCache()
        {
            LoadCacheCommand command = new LoadCacheCommand();        
            await CreateDataCacheLocal(HttpContext, command);
            ResponseBase<LoadCacheResponse> objResponse = await _mediator.Send(command);
            return OkUrban(objResponse);
        }
    }
}
