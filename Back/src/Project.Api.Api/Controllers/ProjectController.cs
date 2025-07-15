using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Application.Commands.AppProject;

namespace Project.Api.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        [ProducesResponseType(typeof(bool), 201)]
        public async Task<IActionResult> PostProject(
            [FromBody] CreateProjectCommand command
        )
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return new BadRequestObjectResult(result.Data);
            }
            return new OkObjectResult(result.Data);
        }

        [HttpGet("getAll")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> GetAllProjects(
            [FromQuery] GetAllProjectQuery query
        )
        {
            var result = await _mediator.Send(query);

            if (!result.Success)
            {
                return new BadRequestObjectResult(result.Data);
            }
            return new OkObjectResult(result.Data);
        }

        [HttpGet("getByUuid")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> GetByUuidProjects(
        [FromQuery] GetByUuidProjectQuery query)
        {
            var result = await _mediator.Send(query);

            if (!result.Success)
            {
                return new BadRequestObjectResult(result.Data);
            }
            return new OkObjectResult(result.Data);
        }

        [HttpDelete()]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> GetByUuidProjects(
        [FromQuery] RemoveProjectQuery query)
        {
            var result = await _mediator.Send(query);

            if (!result.Success)
            {
                return new BadRequestObjectResult(result.Data);
            }
            return new OkObjectResult(result.Data);
        }
    }
}