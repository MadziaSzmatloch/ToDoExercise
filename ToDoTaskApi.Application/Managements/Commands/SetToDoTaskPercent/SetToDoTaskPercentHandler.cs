using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ToDoTaskApi.Application.DTO;
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
                throw new Exception("not found"); //TODO: exception

            task.PercentOfCompletness = request.percentage;

            if (await _toDoTaskRepository.Update(task))
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
