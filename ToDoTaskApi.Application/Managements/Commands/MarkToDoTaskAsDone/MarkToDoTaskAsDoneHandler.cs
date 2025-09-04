using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ToDoTaskApi.Application.DTO;
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
            var task = await _toDoTaskRepository.GetById(request.id);

            if (task == null)
                throw new Exception("not found"); //TODO: exception

            task.IsCompleted = true;

            if (!await _toDoTaskRepository.Update(task))
            {
                throw new Exception("Not found"); //TODO excpetion
            }

            return mapper.ToDoTaskToToDoTaskDto(task);
        }
    }
}
