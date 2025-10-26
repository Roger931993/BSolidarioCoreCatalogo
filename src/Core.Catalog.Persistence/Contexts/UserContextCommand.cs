using Core.Catalog.Domain.Interfaces.Dapper;
using Core.Catalog.Persistence.Repositories.Dapper.Common;

namespace Core.Catalog.Persistence.Contexts
{
    public class UserContextCommand : DbContextDapperCommon
    {
        public UserContextCommand(IDatabaseConnect options) : base(options.GetConnection("Urban_Stamp"))
        {
        }
    }
}
