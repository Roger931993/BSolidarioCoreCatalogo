namespace Core.Catalog.Domain.Common.Dapper
{
    public class DataAnnotationSpNameAttribute :Attribute
    {
        public string SpName { get;}
        public DataAnnotationSpNameAttribute(string spName)
        {
            SpName = spName;
        }
    }
}
