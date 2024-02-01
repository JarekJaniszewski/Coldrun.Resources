using Resources.Contracts;

namespace Resources.Application.TruckManagement.Queries.GetTruck
{
    public class GetTruckCommand : IValidatableRequest<GetTruckResponse>
    {
        public string Code { get; set; } = string.Empty;
    }
}
