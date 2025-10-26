using Core.Catalog.Domain.Common.Dapper;

namespace Core.Catalog.Domain.Interfaces.Dapper
{
    public interface IUnitOfWork
  {
    IRepositoryCommand<TDomainEntity> RepositoryCommand<TDomainEntity>() where TDomainEntity : Entity;


    IRepositoryProcedures<TDomainEntity> RepositoryProcedure<TDomainEntity>() where TDomainEntity : EntitySp;
    void CommitTransaction();
    void RollBackTransaction();

    void BeginTransaccion();
  }
}
