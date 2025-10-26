namespace Core.Catalog.Domain.Entities
{
    public class catalog_error: BaseEntity
    {
        public int catalog_error_id { get; set; }
        public string? error_description { get; set; }
        public string? error_priority { get; set; }
        public int error_status_code { get; set; }
    }
}
