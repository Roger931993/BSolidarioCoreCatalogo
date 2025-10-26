using AutoMapper;
using Core.Catalog.Application.DTOs;
using Core.Catalog.Application.DTOs.Base;
using Core.Catalog.Application.Features.Parametros.Queries;
using Core.Catalog.Application.Interfaces.Base;
using Core.Catalog.Application.Interfaces.Infraestructure;
using Core.Catalog.Application.Interfaces.Persistence;
using Core.Catalog.Domain.Common;
using Core.Catalog.Domain.Entities;
using static Core.Catalog.Model.Entity.EnumTypes;

namespace Core.Catalog.Application.Features.Parametros.Handlers
{
    public class GetParametroQueryHandler : BaseCommand, IDecoradorRequestHandler<GetParametroQuery, ResponseBase<GetParametroResponse>>
    {
        private readonly ICatalogRepository _catalogRepository;

        public GetParametroQueryHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICatalogRepository catalogRepository) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._catalogRepository = catalogRepository;
        }

        public async Task<ResponseBase<GetParametroResponse>> Handle(GetParametroQuery request, CancellationToken cancellationToken)
        {
            GetParametroRequest RequestData = request.request.Request!;
            Guid IdTraking = (Guid)request.request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            GetParametroResponse objResponse = new GetParametroResponse();
            
            try
            {                
                parametroDto objResult = new parametroDto();
                parametros objSaved = _catalogRepository.GetIncludesAsNoTraking<parametros>().FirstOrDefault(x => x.parametros_id == RequestData.parametro_id)!;

                if (objSaved == null)
                    return await ErrorResponse<GetParametroResponse>(IdTraking, (int)TypeError.NoData,Status: 500);
                objResult = _mapper.Map<parametroDto>(objSaved);
                objResponse.parametro = objResult;               
            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<GetParametroResponse>(IdTraking, ex, (int)TypeError.InternalError,Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
