using Resources.Contracts;

namespace Resources.Application.TruckManagement.Queries.TrucksSearch
{
    public class TrucksSearchQueryCommand : IValidatableRequest<TrucksSearchQueryResponse>
    {
        public string Filters { get; set; } = string.Empty;

        public string Sorting { get; set; } = string.Empty;

        public Paging Paging { get; set; } = new Paging();
    }
}
