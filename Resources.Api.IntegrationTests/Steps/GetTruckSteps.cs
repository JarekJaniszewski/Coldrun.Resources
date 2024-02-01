using FluentAssertions;
using Newtonsoft.Json;
using Resources.Api.IntegrationTests.Repositories.Models;
using Resources.Application.TruckManagement.Queries.GetTruck;
using TechTalk.SpecFlow;

namespace Resources.Api.IntegrationTests.Steps
{
    [Binding]
    public class GetTruckSteps : BaseSteps
    {
        public GetTruckSteps(ScenarioContext scenarioContext, ScenarioHelper scenario)
            : base(scenarioContext, scenario)
        {
        }


        [When(@"I send a request of get a Truck")]
        public async Task WhenISendARequestOfGetATruck()
        {
            var createTrucksDto = Context.Get<List<CreateTruckDto>>(Constants.ContextKeys.CreateTruckDto);

            await GetTruckAsync(new GetTruckCommand { Code = createTrucksDto.First().Code});
        }

        [Then(@"I get a truck detail as a response")]
        public void ThenIGetATruckDetailAsAResponse()
        {
            var createTrucksDto = Context.Get< List<CreateTruckDto>>(Constants.ContextKeys.CreateTruckDto);
            var createTruckDto = createTrucksDto.First();

            var response = Scenario.Response.Content;
            var result = JsonConvert.DeserializeObject<GetTruckResponse>(response);

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().Be(createTruckDto.Name);
            result.Code.Should().Be(createTruckDto.Code);
            result.Description.Should().Be(createTruckDto.Description);
        }

        private async Task GetTruckAsync(GetTruckCommand getTruckCommand)
        {
            var url = GetGetTruckUrl(getTruckCommand.Code);
            var response = await HttpClient.GetAsync(url, CancellationToken.None);
            var content = await response.Content.ReadAsStringAsync();

            Scenario.Response = (content, response.StatusCode);
        }

        private static string GetGetTruckUrl(string code)
        {
            return $"/api/trucks/get-truck?code=" + code;
        }
    }
}
