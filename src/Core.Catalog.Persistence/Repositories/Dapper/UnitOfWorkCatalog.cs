using Core.Catalog.Application.Interfaces.Persistence;
using Core.Catalog.Domain.Interfaces.Dapper;
using Core.Catalog.Persistence.Contexts;
using Core.Catalog.Persistence.Repositories.Dapper.Common;

namespace Core.Catalog.Persistence.Repositories.Dapper
{
    public class UnitOfWorkCatalog : UnitOfWork, IUnitOfWorkCatalog
    {
        private readonly IDatabaseConnect _options;
      
        public UnitOfWorkCatalog(UserContextCommand contextDapper, IDatabaseConnect options) : base(contextDapper)
        {
            _options = options;
        }
           
    }
}
