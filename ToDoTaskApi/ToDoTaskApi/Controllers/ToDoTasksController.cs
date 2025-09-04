using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using ToDoTaskApi.Application.Managements.Commands.CreateToDoTask;
using ToDoTaskApi.Application.Managements.Commands.UpdateToDoTask;
using ToDoTaskApi.Application.Managements.Queries.GetAllToDoTask;
using ToDoTaskApi.Application.Managements.Queries.GetToDoTaskById;

namespace ToDoTaskApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoTasksController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _mediator.Send(new GetAllToDoTasksRequest());
            return Ok(tasks);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = new GetToDoTaskByIdRequest(id);
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateToDoTaskRequest request)
        {
            return Ok(await _mediator.Send(request)); ;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateToDoTaskRequest request)
        {
            return Ok(await _mediator.Send(request)); ;
        }
    }
}
