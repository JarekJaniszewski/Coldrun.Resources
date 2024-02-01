using Resources.Application.TruckManagement.Commands.UpdateTruck;
using Swashbuckle.AspNetCore.Filters;

namespace Resources.Api.Swagger
{
    public class UpdateTruckCommandExample : IExamplesProvider<UpdateTruckCommand>
    {
        public UpdateTruckCommand GetExamples()
        {
            return new UpdateTruckCommand
            {
                Code = "ASD2322D3",
                Name = "TruckName",
                Status = 0,
                Description = "TruckDescriptions",
            };
        }
    }
}
