using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ToDoTaskApi.Application.DTO;
using ToDoTaskApi.Application.Mappers;
using ToDoTaskApi.Domain.Interfaces;

namespace ToDoTaskApi.Application.Managements.Commands.UpdateToDoTask
{
    public class UpdateToDoTaskHandler(IToDoTaskRepository toDoTaskRepository) : IRequestHandler<UpdateToDoTaskRequest, ToDoTaskDTO>
    {
        private readonly IToDoTaskRepository _toDoTaskRepository = toDoTaskRepository;

        public async Task<ToDoTaskDTO> Handle(UpdateToDoTaskRequest request, CancellationToken cancellationToken)
        {
            var mapper = new ToDoTaskMapper();
            var task = mapper.ToDoTaskDtoToToDoTask(request.task);

            if(await _toDoTaskRepository.Update(task))
            {
                return mapper.ToDoTaskToToDoTaskDto(task);
            }
            else
            {
                throw new Exception("Not found"); //TODO excpetion
            }
           
        }
    }
}
