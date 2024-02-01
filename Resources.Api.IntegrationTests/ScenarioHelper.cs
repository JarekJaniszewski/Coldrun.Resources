using System.Net;
using TechTalk.SpecFlow;

namespace Resources.Api.IntegrationTests
{
    public class ScenarioHelper
    {
        private readonly ScenarioContext _context;

        public ScenarioHelper(ScenarioContext scenario)
        {
            _context = scenario;
        }

        internal (string Content, HttpStatusCode Status) Response
        {
            get => _context.Get<(string Content, HttpStatusCode Status)>("response");
            set => _context.Set(value, "response");
        }
    }
}
