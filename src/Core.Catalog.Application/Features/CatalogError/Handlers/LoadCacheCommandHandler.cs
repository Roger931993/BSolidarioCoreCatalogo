using AutoMapper;
using Core.Catalog.Application.DTOs;
using Core.Catalog.Application.DTOs.Base;
using Core.Catalog.Application.Features.CatalogError.Commands;
using Core.Catalog.Application.Interfaces.Base;
using Core.Catalog.Application.Interfaces.Infraestructure;
using Core.Catalog.Application.Interfaces.Persistence;
using Core.Catalog.Domain.Common;
using Core.Catalog.Domain.Entities;
using static Core.Catalog.Model.Entity.EnumTypes;

namespace Core.Catalog.Application.Features.CatalogError.Handlers
{
    public class LoadCacheCommandHandler : BaseCommand, IDecoradorRequestHandler<LoadCacheCommand, ResponseBase<LoadCacheResponse>>
    {
        private readonly ICatalogRepository _catalogRepository;

        public LoadCacheCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICatalogRepository catalogRepository) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._catalogRepository = catalogRepository;
        }

        public async Task<ResponseBase<LoadCacheResponse>> Handle(LoadCacheCommand request, CancellationToken cancellationToken)
        {
            LoadCacheRequest RequestData = request.Request!;
            Guid IdTraking = (Guid)request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            LoadCacheResponse objResponse = new LoadCacheResponse();
            try
            {
                List<catalog_error> objSaved = _catalogRepository.GetIncludesAsNoTraking<catalog_error>().Where(x => x.status != (int)TypeStatus.Disable).ToList()!;

                List<catalog_errorDto> objErrors = _mapper.Map<List<catalog_errorDto>>(objSaved);

                await _redisCache.SetAsync<List<catalog_errorDto>>("catalog_error", objErrors, 1440);                
            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<LoadCacheResponse>(IdTraking,ex, (int)TypeError.InternalError, ex.Message, 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
