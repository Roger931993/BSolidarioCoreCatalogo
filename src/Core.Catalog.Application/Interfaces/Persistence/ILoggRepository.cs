using Core.Catalog.Domain.Entities;
using Core.Catalog.Domain.Models;

namespace Core.Catalog.Application.Interfaces.Persistence
{
    public interface ILoggRepository
    {      
        Task<api_log_catalog_header> SaveHeader(LoggingMdl model);
        Task<List<api_log_catalog_detail>> SaveDetails(List<api_log_catalog_detail> model);
    }
}
