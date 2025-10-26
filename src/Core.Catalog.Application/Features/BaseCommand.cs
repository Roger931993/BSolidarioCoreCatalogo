using AutoMapper;
using Core.Catalog.Application.Common;
using Core.Catalog.Application.DTOs.Base;
using Core.Catalog.Application.Interfaces.Base;
using Core.Catalog.Application.Interfaces.Infraestructure;
using Core.Catalog.Domain.Common;
using Humanizer;
using Newtonsoft.Json;
using System.Xml.Serialization;
using static Core.Catalog.Model.Entity.EnumTypes;

namespace Core.Catalog.Application.Features
{
    public class BaseCommand
    {
        public readonly IErrorCatalogException _errorCatalogException;
        public readonly IRedisCache _redisCache;
        public readonly IMemoryCacheLocalService _memoryCacheLocalService;
        public readonly IMapper _mapper;

        public BaseCommand(IErrorCatalogException errorCatalogException, IRedisCache redisCache, IMemoryCacheLocalService memoryCacheLocalService, IMapper mapper)
        {
            this._errorCatalogException = errorCatalogException;
            this._redisCache = redisCache;
            this._memoryCacheLocalService = memoryCacheLocalService;
            this._mapper = mapper;
        }
        public async Task<ResponseBase<T>> OkResponse<T>(T response)
        {
            return new ResponseBase<T>()
            {
                data = response,
                error = new Error
                {
                    codeError = (int)TypeError.Ok,
                    success = true,
                }
            };
        }


        public async Task<ResponseBase<T>> OkResponse<T>(T response, int pageNumber, int pageSize, int totalRecords)
        {
            return new ResponseBase<T>()
            {
                data = response,
                pages = new InfoPages()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalRecords = totalRecords
                },
                error = new Error
                {
                    codeError = (int)TypeError.Ok,
                    success = true,
                }
            };
        }

        public async Task<ResponseBase<T>> ErrorResponse<T>(Guid? idTraking, int codeError, string messageError = "", int Status = 500)
        {
            if (string.IsNullOrEmpty(messageError))
                await _errorCatalogException.SetCodeError(codeError, idTraking);
            else
                _errorCatalogException.SetCodeErrorMessage(codeError, messageError, idTraking, Status);
            return new ResponseBase<T>
            {
                errorCatalogException = (ErrorCatalogException)_errorCatalogException,
                error = new Error
                {
                    codeError = _errorCatalogException.CodeError,
                    success = false,
                    messageError = _errorCatalogException.MessageError
                }
            };
        }

        public async Task<ResponseBase<T>> ErrorResponseEx<T>(Guid? idTraking, Exception ex, int codeError, string messageError = "", int Status = 500)
        {
            if (string.IsNullOrEmpty(messageError))
                await _errorCatalogException.SetCodeError(codeError, idTraking, ex);
            else
                _errorCatalogException.SetCodeErrorMessage(codeError, ex, messageError, idTraking, Status);
            return new ResponseBase<T>
            {
                errorCatalogException = (ErrorCatalogException)_errorCatalogException,
                error = new Error
                {
                    codeError = _errorCatalogException.CodeError,
                    success = false,
                    messageError = _errorCatalogException.MessageError
                }
            };
        }

        public async Task<T> ObtenerCatalogoCache<T>(string id)
        {
            object data = await _memoryCacheLocalService.GetCachedDataObject(id);
            return (T)data;
        }


        public async Task AddLogInputXml<TRequest>(TRequest? request, int statusCode, DataCacheLocal dataCache)
        {
            // Crear un objeto XmlSerializer para la clase TRequest
            XmlSerializer serializer = new XmlSerializer(typeof(TRequest));
            string xmlString = string.Empty;
            // Crear un StringWriter para escribir el XML en un string
            using (StringWriter writer = new StringWriter())
            {
                // Serializar el objeto a XML y escribirlo en el StringWriter
                serializer.Serialize(writer, request);

                // Obtener el XML como un string
                xmlString = writer.ToString();
            }
            dataCache.AddLogProcces(new DataCacheLocalProcess()
            {
                TypeProcess = "In",
                ProcessComponent = "BusinessLogic",
                CreatedAt = DateTime.UtcNow,
                StatusCode = statusCode,
                DataMessage = xmlString,
            });
        }

        public async Task AddLogError<TRequest>(TRequest? request, int statusCode, Exception ex, DataCacheLocal dataCache)
        {
            var st = new System.Diagnostics.StackTrace(ex, true);
            var frame = st.GetFrame(0); // Primer nivel del stack
            var method = frame?.GetMethod();
            var className = method?.DeclaringType?.FullName;
            var methodName = method?.Name;
            string origin = $"Clase: {className}, Método: {methodName}";
            dataCache.AddLogProcces(new DataCacheLocalProcess()
            {
                TypeProcess = "TaskMethod",
                ProcessComponent = "BusinessLogic",
                CreatedAt = DateTime.UtcNow,
                StatusCode = statusCode,
                DataMessage = $"{origin}: {JsonConvert.SerializeObject(ex, Formatting.Indented)}",
            });
        }

        public string ConvertMontosALetras(decimal monto, string cultura, string descripcionMoneda, string Moneda)
        {
            // Convertir la parte entera a palabras
            long parteEntera = (long)monto;
            int decimales = (int)((monto - parteEntera) * 100);


            // Convierte la parte entera a palabras en español con Humanizer
            string montoEnLetras = parteEntera.ToWords(new System.Globalization.CultureInfo(cultura));

            // Formatear el resultado en el estilo deseado (ej., "mil doscientos treinta y cuatro con 56/100")
            montoEnLetras = $"{montoEnLetras} {descripcionMoneda} {decimales:D2}/100 {Moneda}";
            return montoEnLetras.ToUpper();
        }
    }
}
