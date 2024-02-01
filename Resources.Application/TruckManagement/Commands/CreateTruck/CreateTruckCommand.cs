using Resources.Contracts;
using Resources.Domain.Enums;
using System.Text.Json.Serialization;

namespace Resources.Application.TruckManagement.Commands.CreateTruck
{
    public class CreateTruckCommand : IValidatableRequest<CreateTruckResponse>
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusType Status { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
