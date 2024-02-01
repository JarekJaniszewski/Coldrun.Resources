using Resources.Domain.Enums;

namespace Resources.Domain.Models
{
    public class CreateTruckDto
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public StatusType Status { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
