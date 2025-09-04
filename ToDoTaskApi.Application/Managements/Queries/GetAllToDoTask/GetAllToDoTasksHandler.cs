using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ToDoTaskApi.Application.DTO;
using ToDoTaskApi.Application.Mappers;
using ToDoTaskApi.Domain.Interfaces;

namespace ToDoTaskApi.Application.Managements.Queries.GetAllToDoTask
{
    public class GetAllToDoTasksHandler(IToDoTaskRepository toDoTaskRepository) : IRequestHandler<GetAllToDoTasksRequest, IEnumerable<ToDoTaskDTO>>
    {
        private readonly IToDoTaskRepository _toDoTaskRepository = toDoTaskRepository;

        public async Task<IEnumerable<ToDoTaskDTO>> Handle(GetAllToDoTasksRequest request, CancellationToken cancellationToken)
        {
            var tasks = await _toDoTaskRepository.GetAll();
            var mapper = new ToDoTaskMapper();
            return tasks.Select(mapper.ToDoTaskToToDoTaskDto).ToList();
        }
    }
}
