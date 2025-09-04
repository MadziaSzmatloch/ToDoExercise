using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using ToDoTaskApi.Application.Managements.Commands.CreateToDoTask;

namespace ToDoTaskApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoTasksController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateToDoTaskRequest request)
        {
            var t = await _mediator.Send(request);
            return Ok(t);
        }
    }
}
