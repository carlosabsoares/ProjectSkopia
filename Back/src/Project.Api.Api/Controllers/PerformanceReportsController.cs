using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Application.Commands.AppReports;

namespace Project.Api.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PerformanceReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PerformanceReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> GetAllTasks(
            [FromQuery] GetPerformanceReportQuery query
        )
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