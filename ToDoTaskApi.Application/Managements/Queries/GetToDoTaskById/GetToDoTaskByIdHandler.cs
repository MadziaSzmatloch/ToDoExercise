using MediatR;
using ToDoTaskApi.Abstractions.Exceptions;
using ToDoTaskApi.Application.DTO;
using ToDoTaskApi.Application.Mappers;
using ToDoTaskApi.Domain.Interfaces;

namespace ToDoTaskApi.Application.Managements.Queries.GetToDoTaskById
{
    public class GetToDoTaskByIdHandler(IToDoTaskRepository toDoTaskRepository) : IRequestHandler<GetToDoTaskByIdRequest, ToDoTaskDTO>
    {
        private readonly IToDoTaskRepository _toDoTaskRepository = toDoTaskRepository;

        public async Task<ToDoTaskDTO> Handle(GetToDoTaskByIdRequest request, CancellationToken cancellationToken)
        {
            var task = await _toDoTaskRepository.GetById(request.Id);
            if (task == null)
            {
                throw new NotFoundException("Task with given id doesn't exist");
            }

            var mapper = new ToDoTaskMapper();
            return mapper.ToDoTaskToToDoTaskDto(task);
        }
    }
}
