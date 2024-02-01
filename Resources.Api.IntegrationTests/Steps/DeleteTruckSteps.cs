using FluentAssertions;
using Newtonsoft.Json;
using Resources.Api.IntegrationTests.Repositories;
using Resources.Application.TruckManagement.Commands.DeleteTruck;
using TechTalk.SpecFlow;

namespace Resources.Api.IntegrationTests.Steps
{
    [Binding]
    public class DeleteTruckSteps : BaseSteps
    {
        public DeleteTruckSteps(ScenarioContext scenarioContext, ScenarioHelper scenario)
            : base(scenarioContext, scenario)
        {
        }

        [When(@"I delete the truck with code '([^']*)'")]
        public async Task WhenIDeleteTheTruckWithCode(string code)
        {
            await DeleteTruckAsync(new DeleteTruckCommand { Code = code });
            Context.Set(code, Constants.ContextKeys.DeleteTruckCommand);
        }

        [Then(@"Truck is deleted")]
        public void ThenTruckIsDeleted()
        {
            var code = Context.Get<string>(Constants.ContextKeys.DeleteTruckCommand);

            var response = Scenario.Response.Content;
            var result = JsonConvert.DeserializeObject<bool>(response);
            result.Should().BeTrue();

            var trucks = TruckRepository.GetTrucks();
            trucks.Any(x => x.Code == code).Should().BeFalse();
        }

        private async Task DeleteTruckAsync(DeleteTruckCommand getTruckCommand)
        {
            var url = DeleteGetTruckUrl(getTruckCommand.Code);
            var response = await HttpClient.DeleteAsync(url, CancellationToken.None);
            var content = await response.Content.ReadAsStringAsync();

            Scenario.Response = (content, response.StatusCode);
        }

        private static string DeleteGetTruckUrl(string code)
        {
            return $"/api/trucks/delete-truck?code=" + code;
        }
    }
}
