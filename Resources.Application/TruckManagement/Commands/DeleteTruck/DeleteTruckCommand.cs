using Resources.Contracts;

namespace Resources.Application.TruckManagement.Commands.DeleteTruck
{
    public class DeleteTruckCommand : IValidatableRequest<bool>
    {
        public string Code { get; set; } = string.Empty;
    }
}
