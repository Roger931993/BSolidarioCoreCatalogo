using System.Data;

namespace Core.Catalog.Domain.Common.Dapper
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
  public class SpParametersCommonAttribute : Attribute
  {
    public string ParamName { get; }
    public DbType DbType { get; }
    public ParameterDirection Direction { get; }
    public string? CustomDbType { get; set; }
    public SpParametersCommonAttribute(string paramName, DbType dbtype, ParameterDirection direction, string? customDbType = null)
    {
      ParamName = paramName;
      DbType = dbtype;
      Direction = direction;
      CustomDbType = customDbType;
    }




  }
}
