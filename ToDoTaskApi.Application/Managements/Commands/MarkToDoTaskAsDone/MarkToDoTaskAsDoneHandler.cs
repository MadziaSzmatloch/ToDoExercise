using MediatR;
using ToDoTaskApi.Application.DTO;
using ToDoTaskApi.Abstractions.Exceptions;
using ToDoTaskApi.Application.Mappers;
using ToDoTaskApi.Domain.Interfaces;

namespace ToDoTaskApi.Application.Managements.Commands.MarkToDoTaskAsDone
{
    public class MarkToDoTaskAsDoneHandler(IToDoTaskRepository toDoTaskRepository): IRequestHandler<MarkToDoTaskAsDoneRequest, ToDoTaskDTO>
    {
        private readonly IToDoTaskRepository _toDoTaskRepository = toDoTaskRepository;

        public async Task<ToDoTaskDTO> Handle(MarkToDoTaskAsDoneRequest request, CancellationToken cancellationToken)
        {
            var mapper = new ToDoTaskMapper();
            var task = await _toDoTaskRepository.GetById(request.id) ?? throw new NotFoundException("Task with given id doesn't exist");

            task.IsCompleted = true;
            task.PercentOfCompletness = 100;

            if (!await _toDoTaskRepository.Update(task))
            {
                throw new NotFoundException("Task with given id doesn't exist");
            }

            return mapper.ToDoTaskToToDoTaskDto(task);
        }
    }
}
