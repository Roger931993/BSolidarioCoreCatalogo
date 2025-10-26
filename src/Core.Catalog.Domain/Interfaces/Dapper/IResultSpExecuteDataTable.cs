using Dapper;
using System.Data;

namespace Core.Catalog.Domain.Interfaces.Dapper
{
  public interface IResultSpExecuteDataTable
  {
    void SetResultSp(DataTable value);
    void SetParameters(DynamicParameters value);
  }
}
