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
    public class RegisterErrorCommandHandler : BaseCommand, IDecoradorRequestHandler<RegisterErrorCommand, ResponseBase<RegisterErrorResponse>>
    {
        private readonly ICatalogRepository _catalogRepository;

        public RegisterErrorCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICatalogRepository catalogRepository) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._catalogRepository = catalogRepository;
        }

        public async Task<ResponseBase<RegisterErrorResponse>> Handle(RegisterErrorCommand request, CancellationToken cancellationToken)
        {
            RegisterErrorRequest RequestData = request.Request!;
            Guid IdTraking = (Guid)request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            RegisterErrorResponse objResponse = new RegisterErrorResponse();
            try
            {
                catalog_error objSaved = _catalogRepository.GetIncludesAsNoTraking<catalog_error>().FirstOrDefault(x =>(x.catalog_error_id == RequestData.catalog_error_id || x.error_description!.ToUpper() == RequestData.error_description!.ToUpper()))!;

                if (objSaved == null)
                {
                    catalog_error objNew = new catalog_error()
                    {
                        error_description = RequestData.error_description,
                        error_status_code = RequestData.error_status_code ?? 0,
                        error_priority = RequestData.error_priority,                        
                        status = (int)TypeStatus.Active,
                       
                    };
                    objNew = await _catalogRepository.Save(objNew);
                    objResponse.catalog_error = _mapper.Map<catalog_errorDto>(objNew);
                }
                else
                {
                    List<string> camposForzarModificacion = new List<string>();
                    camposForzarModificacion.Add("error_status_code");           

                    objSaved.error_description = RequestData.error_description;
                    objSaved.error_status_code = RequestData.error_status_code ?? 0;
                    objSaved.error_priority = RequestData.error_priority;
                    objSaved.status = (int)TypeStatus.Active;

                    objSaved = await _catalogRepository.Update(objSaved.catalog_error_id, objSaved);
                    objResponse.catalog_error = _mapper.Map<catalog_errorDto>(objSaved);
                }                            
            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<RegisterErrorResponse>(IdTraking, ex, (int)TypeError.InternalError, ex.Message, 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
