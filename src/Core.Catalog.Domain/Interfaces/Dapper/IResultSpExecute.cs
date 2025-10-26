using Dapper;

namespace Core.Catalog.Domain.Interfaces.Dapper
{
  public interface IResultSpExecute
  {
    int ResultSp { get; }
    DynamicParameters Parameters { get; }
    void SetResultSp(int value);
    void SetParameters(DynamicParameters value);
  }
}
