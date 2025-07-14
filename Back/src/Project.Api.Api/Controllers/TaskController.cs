using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Application.Commands.AppProject;
using Project.Api.Application.Commands.AppTask;

namespace Project.Api.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        [ProducesResponseType(typeof(bool), 201)]
        public async Task<IActionResult> PostTask(
            [FromBody] CreateTaskCommand command
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
        public async Task<IActionResult> GetAllTasks(
            [FromQuery] GetAllTaskQuery query
        )
        {
            var result = await _mediator.Send(query);

            if (!result.Success)
            {
                return new BadRequestObjectResult(result.Data);
            }
            return new OkObjectResult(result.Data);
        }

        [HttpPut()]
        [ProducesResponseType(typeof(bool), 202)]
        public async Task<IActionResult> UpdateCategory(
        [FromBody] UpdateTaskCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return new BadRequestObjectResult(result.Data);
            }
            return new OkObjectResult(result.Data);
        }

        [HttpDelete()]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> GetByUuidTasks(
        [FromQuery] RemoveTaskQuery query)
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