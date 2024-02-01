using FluentAssertions;
using Newtonsoft.Json;
using Resources.Application.TruckManagement.Commands.CreateTruck;
using System.Net.Http.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Resources.Api.IntegrationTests.Steps
{
    [Binding]
    public class CreateTruckSteps: BaseSteps
    {

        public CreateTruckSteps(ScenarioContext scenarioContext, ScenarioHelper scenario)
            : base(scenarioContext, scenario)
        {

        }

        [Given(@"I have a new Truck")]
        public Task GivenIHaveANewTruck(Table table)
        {
            var createTruckCommand = table.CreateSet<CreateTruckCommand>().Single();

            Context.Set(createTruckCommand, Constants.ContextKeys.CreateTruckCommand);
            return Task.CompletedTask;
        }

        [When(@"I send a request of create a Truck")]
        public async Task WhenISendARequestOfCreateATruck()
        {
            var createTruckCommand = Context.Get<CreateTruckCommand>(Constants.ContextKeys.CreateTruckCommand);
            await CreateTruckAsync(createTruckCommand);
        }

        [Then(@"truck added to database")]
        public Task ThenTruckAddedToDatabase()
        {
            var createTruckCommand = Context.Get<CreateTruckCommand>(Constants.ContextKeys.CreateTruckCommand);
            var response = Scenario.Response.Content;
            var result = JsonConvert.DeserializeObject<CreateTruckResponse>(response);

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().Be(createTruckCommand.Name);
            result.Code.Should().Be(createTruckCommand.Code);
            result.Description.Should().Be(createTruckCommand.Description);
            return Task.CompletedTask;
        }

        private async Task CreateTruckAsync(CreateTruckCommand createTruckCommand)
        {
            var url = GetCreateTruckUrl();

            var response = await HttpClient.PostAsJsonAsync(url, createTruckCommand, CancellationToken.None);
            var content = await response.Content.ReadAsStringAsync();

            Scenario.Response = (content, response.StatusCode);
        }

        private static string GetCreateTruckUrl()
        {
            return $"/api/trucks/create-truck";
        }
    }
}
