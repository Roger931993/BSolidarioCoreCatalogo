using AutoMapper;
using Core.Catalog.Application.DTOs;
using Core.Catalog.Application.DTOs.Base;
using Core.Catalog.Application.Features.Parametros.Commands;
using Core.Catalog.Application.Interfaces.Base;
using Core.Catalog.Application.Interfaces.Infraestructure;
using Core.Catalog.Application.Interfaces.Persistence;
using Core.Catalog.Domain.Common;
using Core.Catalog.Domain.Entities;
using static Core.Catalog.Model.Entity.EnumTypes;

namespace Core.Catalog.Application.Features.Parametros.Handlers
{
    public class LoadParametroCacheCommandHandler : BaseCommand, IDecoradorRequestHandler<LoadParametroCacheCommand, ResponseBase<LoadParametroCacheResponse>>
    {
        private readonly ICatalogRepository _catalogRepository;

        public LoadParametroCacheCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICatalogRepository catalogRepository) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._catalogRepository = catalogRepository;
        }

        public async Task<ResponseBase<LoadParametroCacheResponse>> Handle(LoadParametroCacheCommand request, CancellationToken cancellationToken)
        {
            LoadParametroCacheRequest RequestData = request.Request!;
            Guid IdTraking = (Guid)request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            LoadParametroCacheResponse objResponse = new LoadParametroCacheResponse();
            try
            {
                List<parametros> objSaved = _catalogRepository.GetIncludesAsNoTraking<parametros>().Where(x => x.status != (int)TypeStatus.Disable).ToList()!;

                List<parametroDto> objErrors = _mapper.Map<List<parametroDto>>(objSaved);

                await _redisCache.SetAsync<List<parametroDto>>("parametros", objErrors, 1440);                
            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<LoadParametroCacheResponse>(IdTraking,ex, (int)TypeError.InternalError, ex.Message, 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
