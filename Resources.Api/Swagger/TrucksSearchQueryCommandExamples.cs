using Resources.Application.TruckManagement.Queries.TrucksSearch;
using Resources.Contracts;
using Swashbuckle.AspNetCore.Filters;

namespace Resources.Api.Swagger
{
    public class TrucksSearchQueryCommandExamples : IMultipleExamplesProvider<TrucksSearchQueryCommand>
    {
        public IEnumerable<SwaggerExample<TrucksSearchQueryCommand>> GetExamples()
        {

            yield return SwaggerExample.Create(
                "Example 1",
                 new TrucksSearchQueryCommand()
                 {
                     Filters = "status >= 1",
                     Sorting = "code asc",
                     Paging = new Paging
                     {
                         Page = 1,
                         PageSize = 20
                     }
                 });

            yield return SwaggerExample.Create(
                "Example 2",
                 new TrucksSearchQueryCommand()
                 {
                     Filters = "status >= 1 && status <= 4",
                     Sorting = "code asc, description",
                     Paging = new Paging
                     {
                         Page = 1,
                         PageSize = 20
                     }
                 });

            yield return SwaggerExample.Create(
                "Example 3",
                 new TrucksSearchQueryCommand()
                 {
                     Filters = "code.Contains(\"ASD2322D32\")",
                     Sorting = "description asc",
                     Paging = new Paging
                     {
                         Page = 1,
                         PageSize = 20
                     }
                 });

            yield return SwaggerExample.Create(
            "Example 4",
             new TrucksSearchQueryCommand()
             {
                 Filters = "name == \"TruckName\"",
                 Sorting = "code asc",
                 Paging = new Paging
                 {
                     Page = 1,
                     PageSize = 20
                 }
             });
        }
    }
}
