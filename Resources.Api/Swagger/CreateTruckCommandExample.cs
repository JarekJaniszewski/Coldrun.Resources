using Resources.Application.TruckManagement.Commands.CreateTruck;
using Swashbuckle.AspNetCore.Filters;
namespace Resources.Api.Swagger
{
  
    public class CreateTruckCommandExample : IExamplesProvider<CreateTruckCommand>
    {
        public CreateTruckCommand GetExamples()
        {
            return new CreateTruckCommand
            {
                Code = "ASD2322D3",
                Name = "TruckName",
                Status = 0,
                Description = "TruckDescriptions",
            };
        }
    }
}
