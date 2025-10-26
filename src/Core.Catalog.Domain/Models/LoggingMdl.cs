namespace Core.Catalog.Domain.Models
{
    public class LoggingMdl
    {
        public ApiLogsHeader Header { get; set; } = new ApiLogsHeader();
        public List<ApiLogsDetails> Details { get; set; } = new List<ApiLogsDetails>();
    }
}
