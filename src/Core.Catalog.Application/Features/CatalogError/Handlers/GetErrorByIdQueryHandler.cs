using AutoMapper;
using Core.Catalog.Application.DTOs;
using Core.Catalog.Application.DTOs.Base;
using Core.Catalog.Application.Features.CatalogError.Queries;
using Core.Catalog.Application.Interfaces.Base;
using Core.Catalog.Application.Interfaces.Infraestructure;
using Core.Catalog.Application.Interfaces.Persistence;
using Core.Catalog.Domain.Common;
using Core.Catalog.Domain.Entities;
using static Core.Catalog.Model.Entity.EnumTypes;

namespace Core.Catalog.Application.Features.CatalogError.Handlers
{
    public class GetErrorByIdQueryHandler : BaseCommand, IDecoradorRequestHandler<GetErrorByIdQuery, ResponseBase<GetErrorByIdResponse>>
    {
        private readonly ICatalogRepository _catalogRepository;

        public GetErrorByIdQueryHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICatalogRepository catalogRepository) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._catalogRepository = catalogRepository;
        }

        public async Task<ResponseBase<GetErrorByIdResponse>> Handle(GetErrorByIdQuery request, CancellationToken cancellationToken)
        {
            GetErrorByIdRequest RequestData = request.request.Request!;
            Guid IdTraking = (Guid)request.request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            GetErrorByIdResponse objResponse = new GetErrorByIdResponse();
            
            try
            {                
                catalog_errorDto objResult = new catalog_errorDto();
                catalog_error objSaved = _catalogRepository.GetIncludesAsNoTraking<catalog_error>().FirstOrDefault(x => x.catalog_error_id == RequestData.catalog_error_id)!;

                if (objSaved == null)
                    return await ErrorResponse<GetErrorByIdResponse>(IdTraking, (int)TypeError.NoData,Status: 500);
                objResult = _mapper.Map<catalog_errorDto>(objSaved);
                objResponse.catalog_error = objResult;               
            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<GetErrorByIdResponse>(IdTraking, ex, (int)TypeError.InternalError,Status: 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
