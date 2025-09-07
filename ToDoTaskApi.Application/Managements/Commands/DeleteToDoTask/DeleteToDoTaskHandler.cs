using MediatR;
using ToDoTaskApi.Abstractions.Exceptions;
using ToDoTaskApi.Domain.Interfaces;

namespace ToDoTaskApi.Application.Managements.Commands.DeleteToDoTask
{
    public class DeleteToDoTaskHandler(IToDoTaskRepository toDoTaskRepository) : IRequestHandler<DeleteToDoTaskRequest, Unit>
    {
        private readonly IToDoTaskRepository _toDoTaskRepository = toDoTaskRepository;

        public async Task<Unit> Handle(DeleteToDoTaskRequest request, CancellationToken cancellationToken)
        {
            if(!await _toDoTaskRepository.Delete(request.id))
            {
                throw new NotFoundException("Task with given id doesn't exist");
            }
            return Unit.Value;
        }
    }
}
