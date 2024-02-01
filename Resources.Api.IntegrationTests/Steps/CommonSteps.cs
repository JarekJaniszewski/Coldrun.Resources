using FluentAssertions;
using Resources.Api.IntegrationTests.Repositories;
using Resources.Api.IntegrationTests.Repositories.Models;
using System.Net;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Resources.Api.IntegrationTests.Steps
{
    [Binding]
    internal class CommonSteps : BaseSteps
    {
        public CommonSteps(ScenarioContext scenarioContext, ScenarioHelper scenario) : base(scenarioContext, scenario)
        {
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            var tables = TruckRepository.ExistTruckTable();
            if (tables.Any())
                TruckRepository.ClearTruckTable();

        }


        [Given(@"I have a Trucks in database")]
        public Task GivenIHaveATrucksInDatabase(Table table)
        {
            var createTrucksDto = table.CreateSet<CreateTruckDto>().ToList();

            foreach (var truck in createTrucksDto.Cast<CreateTruckDto>())
            {
                truck.Id = TruckRepository.CreateTruck(truck);
            }

            Context.Set(createTrucksDto, Constants.ContextKeys.CreateTruckDto);
            return Task.CompletedTask;
        }

        [Given("the response has an HTTP status code of (.*)")]
        [When("the response has an HTTP status code of (.*)")]
        [Then("the response has an HTTP status code of (.*)")]
        public void HttpStatusCode(int status)
        {
            Scenario.Response.Status.Should().Be((HttpStatusCode)status);
        }
    }
}
