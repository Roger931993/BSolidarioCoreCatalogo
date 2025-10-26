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
    public class RegisterParametroCommandHandler : BaseCommand, IDecoradorRequestHandler<RegisterParametroCommand, ResponseBase<RegisterParametroResponse>>
    {
        private readonly ICatalogRepository _catalogRepository;

        public RegisterParametroCommandHandler(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper, ICatalogRepository catalogRepository) : base(errorCatalogException, redisCache, memoryCacheLocalService, mapper)
        {
            this._catalogRepository = catalogRepository;
        }

        public async Task<ResponseBase<RegisterParametroResponse>> Handle(RegisterParametroCommand request, CancellationToken cancellationToken)
        {
            RegisterParametroRequest RequestData = request.Request!;
            Guid IdTraking = (Guid)request.IdTraking!;
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraking.ToString());
            RegisterParametroResponse objResponse = new RegisterParametroResponse();
            try
            {
                parametros objSaved = _catalogRepository.GetIncludesAsNoTraking<parametros>().FirstOrDefault(x =>(x.parametros_id == RequestData.parametros_id || x.nombre_parametro!.ToUpper() == RequestData.nombre_parametro!.ToUpper()))!;

                if (objSaved == null)
                {
                    parametros objNew = new parametros()
                    {
                        nombre_parametro = RequestData.nombre_parametro,
                        valor = RequestData.valor,                                              
                        status = (int)TypeStatus.Active,
                       
                    };
                    objNew = await _catalogRepository.Save(objNew);
                    objResponse.parametro = _mapper.Map<parametroDto>(objNew);
                }
                else
                {
                    List<string> camposForzarModificacion = new List<string>();
                    camposForzarModificacion.Add("error_status_code");           

                    objSaved.nombre_parametro = RequestData.nombre_parametro;
                    objSaved.valor = RequestData.valor;                    
                    objSaved.status = (int)TypeStatus.Active;

                    objSaved = await _catalogRepository.Update(objSaved.parametros_id, objSaved);
                    objResponse.parametro = _mapper.Map<parametroDto>(objSaved);
                }                            
            }
            catch (Exception ex)
            {
                await AddLogError(RequestData, 500, ex, cachelocal);
                return await ErrorResponseEx<RegisterParametroResponse>(IdTraking, ex, (int)TypeError.InternalError, ex.Message, 500);
            }
            return await OkResponse(objResponse);
        }
    }
}
