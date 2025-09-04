using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ToDoTaskApi.Application.DTO;
using ToDoTaskApi.Application.Mappers;
using ToDoTaskApi.Domain.Entities;
using ToDoTaskApi.Domain.Interfaces;

namespace ToDoTaskApi.Application.Managements.Commands.CreateToDoTask
{
    public class CreateToDoTaskHandler(IToDoTaskRepository toDoTaskRepository) : IRequestHandler<CreateToDoTaskRequest, ToDoTaskDTO>
    {
        private readonly IToDoTaskRepository _toDoTaskRepository = toDoTaskRepository;

        public async Task<ToDoTaskDTO> Handle(CreateToDoTaskRequest request, CancellationToken cancellationToken)
        {
            var product = new ToDoTask(request.Title, request.Description, request.ExpirationDate);

            await _toDoTaskRepository.Add(product);

            var mapper = new ToDoTaskMapper();
            return mapper.ToDoTaskToToDoTaskDto(product);
        }
    }
}
