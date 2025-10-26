namespace Core.Catalog.Domain.Entities
{
    public class parametros: BaseEntity
    {
        public int parametros_id { get; set; }
        public string? nombre_parametro { get; set; }
        public string? valor { get; set; }
    }
}
