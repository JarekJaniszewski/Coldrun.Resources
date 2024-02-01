using Resources.Domain.Models;

namespace Resources.Application.TruckManagement.Queries.TrucksSearch
{
    public class TrucksSearchQueryResponse
    {
        public IEnumerable<TrucksSearchItemDto> Items { get; set; }
        public int PageCount { get; set; }
        public long RecordCount { get; set; }
    }
}
