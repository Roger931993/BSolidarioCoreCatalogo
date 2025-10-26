using Core.Catalog.Domain.Common.Dapper;
using Dapper;

namespace Core.Catalog.Domain.Interfaces.Dapper
{
    public interface IResponseGridReaderSpExecution<TDomainResponse> where TDomainResponse : EntitySp
  {
    TDomainResponse EntityDomainResponse { get; }
    SqlMapper.GridReader GridReaderResult { get; }
    void SetGridReader(SqlMapper.GridReader dataset);
    void SetEntity(TDomainResponse entity);
  }
}
