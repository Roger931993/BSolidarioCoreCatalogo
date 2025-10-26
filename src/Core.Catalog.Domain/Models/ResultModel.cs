namespace Core.Catalog.Domain.Models
{
    public class ResultModel<T>
    {
        public bool Success { get; set; }
        public List<T> Data { get; set; } = new List<T>();
        public int CodeError { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}