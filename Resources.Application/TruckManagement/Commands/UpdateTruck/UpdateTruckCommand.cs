using Resources.Contracts;
using Resources.Domain.Enums;

namespace Resources.Application.TruckManagement.Commands.UpdateTruck
{
    public class UpdateTruckCommand : IValidatableRequest<UpdateTruckResponse>
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public StatusType Status { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
