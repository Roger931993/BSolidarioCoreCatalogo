using System.Data;

namespace Core.Catalog.Domain.Interfaces.Dapper
{
  public interface IDatabaseConnect
  {
    IDbConnection GetConnection(string coonectionName);
  }
}
