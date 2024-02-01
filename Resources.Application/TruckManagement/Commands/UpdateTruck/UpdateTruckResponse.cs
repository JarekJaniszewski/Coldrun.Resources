using Resources.Domain.Enums;

namespace Resources.Application.TruckManagement.Commands.UpdateTruck
{
    public class UpdateTruckResponse
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public StatusType Status { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
