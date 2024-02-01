using FluentAssertions;
using Newtonsoft.Json;
using Resources.Application.TruckManagement.Queries.TrucksSearch;
using Resources.Domain.Enums;
using System.Net.Http.Json;
using TechTalk.SpecFlow;

namespace Resources.Api.IntegrationTests.Steps
{
    [Binding]
    public class TrucksSearchQuerySteps : BaseSteps
    {
        public TrucksSearchQuerySteps(ScenarioContext scenarioContext, ScenarioHelper scenario)
            : base(scenarioContext, scenario)
        {
        }


        [When(@"I search for trucks which name contain '([^']*)' and sorted by '([^']*)'")]
        public async Task WhenISearchForTrucksWhichNameContainAndSortedBy(string filters, string searchBy)
        {
            Context.Set(filters, Constants.ContextKeys.Filters);

            TrucksSearchQueryCommand trucksSearchQueryCommand = new()
            {
                Filters = $"name.Contains(\"{filters}\")",
                Sorting = searchBy
            };
            await SearchTrucksAsync(trucksSearchQueryCommand);
        }

        [Then(@"I get trucks as a response with the searched name")]
        public void ThenIGetTrucksAsAResponseWithTheSearchedName()
        {
            var filets = Context.Get<string>(Constants.ContextKeys.Filters);

            var response = Scenario.Response.Content;
            var result = JsonConvert.DeserializeObject<TrucksSearchQueryResponse>(response);
            result.Items.All(x => x.Name.Contains(filets)).Should().BeTrue();
        }

        [When(@"I search for trucks which status equal '([^']*)' and sorted by '([^']*)'")]
        public async Task WhenISearchForTrucksWhichStatusEqualAndSortedBy(int filters, string searchBy)
        {
            Context.Set(filters, Constants.ContextKeys.Filters);

            TrucksSearchQueryCommand trucksSearchQueryCommand = new()
            {
                Filters = $"status == {filters}",
                Sorting = searchBy
            };
            await SearchTrucksAsync(trucksSearchQueryCommand);
        }

        [Then(@"I get trucks as a response with the searched status")]
        public void ThenIGetTrucksAsAResponseWithTheSearchedStatus()
        {
            var filets = Context.Get<int>(Constants.ContextKeys.Filters);

            var response = Scenario.Response.Content;
            var result = JsonConvert.DeserializeObject<TrucksSearchQueryResponse>(response);
            result.Items.All(x => x.Status == (StatusType)filets).Should().BeTrue();
        }

        private async Task SearchTrucksAsync(TrucksSearchQueryCommand trucksSearchQueryCommand)
        {
            var url = GetSearchTrucksUrl();
            var response = await HttpClient.PostAsJsonAsync(url, trucksSearchQueryCommand, CancellationToken.None);
            var content = await response.Content.ReadAsStringAsync();

            Scenario.Response = (content, response.StatusCode);
        }

        private static string GetSearchTrucksUrl()
        {
            return $"/api/trucks/search";
        }
    }
}
