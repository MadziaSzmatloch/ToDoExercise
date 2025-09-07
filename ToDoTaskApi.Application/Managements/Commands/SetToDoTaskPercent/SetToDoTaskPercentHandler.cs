using MediatR;
using ToDoTaskApi.Application.DTO;
using ToDoTaskApi.Abstractions.Exceptions;
using ToDoTaskApi.Application.Mappers;
using ToDoTaskApi.Domain.Interfaces;

namespace ToDoTaskApi.Application.Managements.Commands.SetToDoTaskPercent
{
    public class SetToDoTaskPercentHandler(IToDoTaskRepository toDoTaskRepository) : IRequestHandler<SetToDoTaskPercentRequest, ToDoTaskDTO>
    {
        private readonly IToDoTaskRepository _toDoTaskRepository = toDoTaskRepository;

        public async Task<ToDoTaskDTO> Handle(SetToDoTaskPercentRequest request, CancellationToken cancellationToken)
        {
            var mapper = new ToDoTaskMapper();
            var task = await _toDoTaskRepository.GetById(request.id);

            if (task == null)
                throw new NotFoundException("Task with given id doesn't exist");

            task.PercentOfCompletness = request.percentage;

            if(request.percentage == 100)
            {
                task.IsCompleted = true;
            }

            if (await _toDoTaskRepository.Update(task))
            {
                return mapper.ToDoTaskToToDoTaskDto(task);
            }
            else
            {
                throw new NotFoundException("Task with given id doesn't exist");
            }
        }
    }
}
