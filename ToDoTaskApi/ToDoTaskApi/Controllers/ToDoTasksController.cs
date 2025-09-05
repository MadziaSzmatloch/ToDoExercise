using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoTaskApi.Application.Managements.Commands.CreateToDoTask;
using ToDoTaskApi.Application.Managements.Commands.DeleteToDoTask;
using ToDoTaskApi.Application.Managements.Commands.MarkToDoTaskAsDone;
using ToDoTaskApi.Application.Managements.Commands.SetToDoTaskPercent;
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
        [SwaggerOperation(Summary = "Gets all ToDo tasks", Description = "Returns a list of all tasks in the system.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _mediator.Send(new GetAllToDoTasksRequest());
            return Ok(tasks);
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Gets a single ToDo task by ID", Description = "Returns the task matching the specified ID.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var request = new GetToDoTaskByIdRequest(id);
            return Ok(await _mediator.Send(request));
        }
        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new ToDo task", Description = "Adds a new task to the system.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(CreateToDoTaskRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Updates an existing ToDo task", Description = "Updates all properties of a task.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(UpdateToDoTaskRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPatch("setPercent")]
        [SwaggerOperation(Summary = "Sets the completion percentage of a task", Description = "Updates only the PercentOfCompletness property.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SetTaskPercentage(SetToDoTaskPercentRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes a ToDo task by ID", Description = "Removes a task from the system.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteToDoTaskRequest(id);
            return Ok(await _mediator.Send(request));
        }

        [HttpPatch("markAsDone/{id}")]
        [SwaggerOperation(Summary = "Marks a task as completed", Description = "Sets the IsCompleted property to true.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkAsDone(Guid id)
        {
            var request = new MarkToDoTaskAsDoneRequest(id);
            return Ok(await _mediator.Send(request));
        }
    }
}
