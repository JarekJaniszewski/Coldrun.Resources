using MediatR;
using Microsoft.AspNetCore.Mvc;
using Resources.Application.TruckManagement.Commands.CreateTruck;
using Resources.Application.TruckManagement.Commands.DeleteTruck;
using Resources.Application.TruckManagement.Commands.UpdateTruck;
using Resources.Application.TruckManagement.Queries.GetTruck;
using Resources.Application.TruckManagement.Queries.TrucksSearch;
using Resources.Common.SafeGuards;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Resources.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrucksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrucksController(
            IMediator mediator)
        {
            Protector.Against.Null(mediator, nameof(mediator));

            _mediator = mediator;
        }

        [HttpPost("create-truck")]
        [ProducesResponseType(typeof(CreateTruckResponse), StatusCodes.Status200OK)]
        [SwaggerResponse(200, "Create truck", typeof(CreateTruckCommand))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<CreateTruckResponse>> CreateTruck([FromBody] CreateTruckCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("update-truck")]
        [ProducesResponseType(typeof(CreateTruckResponse), StatusCodes.Status200OK)]
        [SwaggerResponse(200, "Update truck", typeof(UpdateTruckCommand))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<UpdateTruckResponse>> UpdateTruck([FromBody] UpdateTruckCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet("get-truck")]
        [ProducesResponseType(typeof(GetTruckResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<GetTruckResponse>> GetTruck([FromQuery] string code)
        {
            var command = new GetTruckCommand
            {
                Code = code 
            };

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("delete-truck")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<bool>> DeleteTruck([FromQuery] string code)
        {
            var command = new DeleteTruckCommand
            {
                Code = code
            };

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(TrucksSearchQueryResponse), StatusCodes.Status200OK)]
        [SwaggerResponse(200, "Search trucks", typeof(TrucksSearchQueryCommand))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<TrucksSearchQueryResponse>> SearchTrucks([FromBody] TrucksSearchQueryCommand query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
