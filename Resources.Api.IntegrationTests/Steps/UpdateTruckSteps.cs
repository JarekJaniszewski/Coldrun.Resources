using FluentAssertions;
using Newtonsoft.Json;
using Resources.Api.IntegrationTests.Repositories.Models;
using Resources.Application.TruckManagement.Commands.UpdateTruck;
using System.Net.Http.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Resources.Api.IntegrationTests.Steps
{
    [Binding]
    public class UpdateTruckSteps : BaseSteps
    {
        public UpdateTruckSteps(ScenarioContext scenarioContext, ScenarioHelper scenario)
            : base(scenarioContext, scenario)
        {
        }

        [When(@"I update Truck properties")]
        public async Task WhenIUpdateTruckProperties(Table table)
        {
            var truckToUpdate = table.CreateSet<UpdateTruckCommand>().Single();
            await UpdateTruckAsync(truckToUpdate);
            Context.Set(truckToUpdate, Constants.ContextKeys.UpdateTruckCommand);
        }

        [Then(@"I get an updated truck properties as a response")]
        public Task ThenIGetAnUpdatedTruckPropertiesAsAResponse()
        {
            var truckToUpdate = Context.Get<UpdateTruckCommand>(Constants.ContextKeys.UpdateTruckCommand);
            var response = Scenario.Response.Content;
            var result = JsonConvert.DeserializeObject<CreateTruckDto>(response);

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().Be(truckToUpdate.Name);
            result.Code.Should().Be(truckToUpdate.Code);
            result.Description.Should().Be(truckToUpdate.Description);
            return Task.CompletedTask;
        }

        private async Task UpdateTruckAsync(UpdateTruckCommand updateTruckCommand)
        {
            var url = GetUpdateTruckUrl();

            var response = await HttpClient.PutAsJsonAsync(url, updateTruckCommand, CancellationToken.None);
            var content = await response.Content.ReadAsStringAsync();
            Scenario.Response = (content, response.StatusCode);
        }

        private static string GetUpdateTruckUrl()
        {
            return $"/api/trucks/update-truck";
        }
    }
}
