using Resources.Domain.Enums;

namespace Resources.Api.IntegrationTests.Repositories.Models
{
    public class CreateTruckDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public StatusType Status { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
