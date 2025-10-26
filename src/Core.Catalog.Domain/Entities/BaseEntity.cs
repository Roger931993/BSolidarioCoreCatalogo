namespace Core.Catalog.Domain.Entities
{
    public class BaseEntity
    {
        public int? status { get; set; }
        public DateTime? date_create { get; set; }
        public DateTime? date_update { get; set; }

    }
}
