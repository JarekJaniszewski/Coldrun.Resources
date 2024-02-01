namespace Resources.Domain.Models
{
    public class TruckSearchQuery
    {
        public string Filters { get; set; } = string.Empty;

        public string Sorting { get; set; } = string.Empty;

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
