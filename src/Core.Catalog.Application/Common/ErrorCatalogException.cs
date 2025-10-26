using Core.Catalog.Application.Interfaces.Base;
using Core.Catalog.Application.Interfaces.Persistence;
using Core.Catalog.Domain.Entities;

namespace Core.Catalog.Application.Common
{
    public class ErrorCatalogException : ApplicationException, IErrorCatalogException
    {
        private readonly ICatalogRepository _HeiferBillingDbContext;

        public ErrorCatalogException(ICatalogRepository HeiferBillingDbContext)
        {
            _HeiferBillingDbContext = HeiferBillingDbContext;
        }

        public int CodeError { get; private set; }
        public int StatusCodeError { get; private set; }
        public string? MessageError { get; private set; }
        public Guid? IdTraking { get; set; }
        public string Exception { get; set; }

        public async Task SetCodeError(int code, Guid? idTraking)
        {
            IdTraking = idTraking;
            catalog_error objResultadoUser = _HeiferBillingDbContext.GetAsNoTracking<catalog_error>().FirstOrDefault(x => x.catalog_error_id == code)!;
            CodeError = code;
            if (objResultadoUser != null)
            {
                MessageError = objResultadoUser!.error_description;
                StatusCodeError = objResultadoUser!.error_status_code;
            }
            else
            {
                MessageError = "Error no encontrado en la base de datos";
                StatusCodeError = 500;
            }
        }

        public async Task SetCodeError(int code, Guid? idTraking, Exception ex)
        {
            IdTraking = idTraking;
            catalog_error objResultadoUser = _HeiferBillingDbContext.GetAsNoTracking<catalog_error>().FirstOrDefault(x => x.catalog_error_id == code)!;
            CodeError = code;
            if (objResultadoUser != null)
            {
                MessageError = objResultadoUser!.error_description;
                StatusCodeError = objResultadoUser!.error_status_code;
                Exception = ex.Message;
            }
            else
            {
                MessageError = "Error no encontrado en la base de datos";
                StatusCodeError = 500;
                Exception = ex.Message;
            }
        }

        public void SetCodeErrorMessage(int code, string? message, Guid? idTraking, int StatusCode = 500)
        {
            IdTraking = idTraking;
            CodeError = code;
            MessageError = message;
            StatusCodeError = StatusCode;
        }
        public void SetCodeErrorMessage(int code, Exception ex, string? message, Guid? idTraking, int StatusCode = 500)
        {
            IdTraking = idTraking;
            CodeError = code;
            MessageError = message;
            StatusCodeError = StatusCode;
            Exception = ex.Message;
        }
    }
}
