using Dapper;

namespace Core.Catalog.Domain.Interfaces.Dapper
{
  public interface IDbContextDapperCommon
  {
    Task<int> ExecuteAddAsync(string query, DynamicParameters parameters);
    Task<int> ExecuteUpdateAsync(string query, DynamicParameters parameters);
    Task<int> ExecuteDeleteAsync(string query, DynamicParameters parameters);  
    Task<int> ExcecuteQueryTextReturnIntAsync(string sqlText, int? commandTimeout = null, object param = null);
    Task<IResultSpExecute> ExecuteSpReturnIntAsync(string spName, DynamicParameters parameters);
    Task<IResultSpExecuteDataSet> ExecuteSpReturnDataSetAsync(string spName, DynamicParameters parameters);
    Task<IResultSpExecuteGridReader> ExecuteSpReturnGridReaderAsync(string spName, DynamicParameters parameters);
    Task<IResultSpExecuteGridReader> ExecuteFunctionReturnGridReaderAsync(string spName, DynamicParameters parameters);
    Task<IResultSpExecuteDataTable> ExecuteSpReturnDataTabeAsync(string spName, DynamicParameters parameters);
    Task<IEnumerable<TDomainEntity>> QueryAsync<TDomainEntity>(string sqlText, object param = null);
    void CommitTransaccion();
    void BeginTransaccion();
    void RollBackTransaction();
  }
}
