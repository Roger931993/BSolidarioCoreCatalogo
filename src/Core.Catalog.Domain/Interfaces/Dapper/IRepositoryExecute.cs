using Core.Catalog.Domain.Common.Dapper;

namespace Core.Catalog.Domain.Interfaces.Dapper
{
    public interface IRepositoryExecute<TDomainEntity> where TDomainEntity : EntityExecuteText
  {
    Task<int> ExecuteQueryIntReturn(TDomainEntity entity, int? commandTimeout = null, object param = null);
  }
}
