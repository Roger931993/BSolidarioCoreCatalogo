using Core.Catalog.Domain.Common.Dapper;
using System.Data;

namespace Core.Catalog.Domain.Interfaces.Dapper
{
    public interface IResponseDataSetSpExecution<TDomainResponse> where TDomainResponse : EntitySp
  {
    TDomainResponse EntityDomainResponse { get; }
    DataSet DataSetResult { get; }
    void SetEntity(TDomainResponse entity);
    void SetDataSet(DataSet dataset);
  }
}
