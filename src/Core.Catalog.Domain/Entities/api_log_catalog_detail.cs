namespace Core.Catalog.Domain.Entities
{
    public class api_log_catalog_detail
    {
        public int api_log_catalog_detail_id { get; set; }
        public DateTime? created_at { get; set; }
        public int? api_log_catalog_header_id { get; set; }// FK correcta
        public int? status_code { get; set; }
        public string? type_process { get; set; }
        public string? data_message { get; set; }
        public string? process_component { get; set; }
        
        public api_log_catalog_header? api_log_catalog_header { get; set; }  // Propiedad de navegación

    }
}
