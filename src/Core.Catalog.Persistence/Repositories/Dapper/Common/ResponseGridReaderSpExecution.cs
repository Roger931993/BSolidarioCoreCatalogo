using Core.Catalog.Domain.Common.Dapper;
using Core.Catalog.Domain.Interfaces.Dapper;
using Dapper;

namespace Core.Catalog.Persistence.Repositories.Dapper.Common
{
    internal class ResponseGridReaderSpExecution<TDomainResponse> : IResponseGridReaderSpExecution<TDomainResponse> where TDomainResponse : EntitySp
  {
    public TDomainResponse EntityDomainResponse { get; private set; }
    public SqlMapper.GridReader GridReaderResult { get; private set; }
    public void SetGridReader(SqlMapper.GridReader dataset)
    {
      GridReaderResult = dataset;
    }
    public void SetEntity(TDomainResponse entity)
    {
      EntityDomainResponse = entity;
    }

  }
}
