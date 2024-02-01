using Microsoft.AspNetCore.Mvc.Testing;
using TechTalk.SpecFlow;

namespace Resources.Api.IntegrationTests.Steps
{
    public abstract class BaseSteps
    {
        protected readonly HttpClient HttpClient;
        protected readonly ScenarioContext Context;
        protected readonly ScenarioHelper Scenario;
        protected BaseSteps(ScenarioContext scenarioContext, ScenarioHelper scenario)
        {
            Context = scenarioContext;
            Scenario = scenario;
            var webApplicationFactory = new WebApplicationFactory<Program>();
            HttpClient = webApplicationFactory.CreateDefaultClient();
            HttpClient.BaseAddress = new Uri("http://localhost:5000");
        }

    }
}
