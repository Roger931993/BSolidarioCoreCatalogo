using AutoMapper;
using Core.Catalog.Application.Interfaces.Persistence;
using Core.Catalog.Domain.Entities;
using Core.Catalog.Domain.Models;
using Core.Catalog.Persistence.Contexts;

namespace Core.Catalog.Persistence.Repositories.EFCore
{
    public class LoggRepository : ILoggRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;
        public LoggRepository(ApplicationDbContext dbcontext, IMapper mapper)
        {
            this._context = dbcontext;
            this.mapper = mapper;
        }
       
        public async Task<api_log_catalog_header> SaveHeader(LoggingMdl model)
        {
            api_log_catalog_header objNewHeader = new api_log_catalog_header()
            {
                created_at = model.Header.CreatedAt,
                id_tracking = model.Header.IdTracking,
                request_method = model.Header.RequestMethod,
                request_url = model.Header.RequestUrl,
                response_code = model.Header.ResponseCode,
            };

            try
            {
                _context.Add(objNewHeader);
                await _context.SaveChangesAsync();
                return objNewHeader;
            }
            catch (Exception ex) { 
            
            }
            return objNewHeader;


        }

        public async Task<List<api_log_catalog_detail>> SaveDetails(List<api_log_catalog_detail> model) 
        {
            try
            {
                await _context.AddRangeAsync(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex) { }
            return model;
        }       
    }
}
