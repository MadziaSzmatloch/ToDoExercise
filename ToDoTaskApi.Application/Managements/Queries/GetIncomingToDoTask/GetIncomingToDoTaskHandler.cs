using MediatR;
using ToDoTaskApi.Application.DTO;
using ToDoTaskApi.Abstractions.Exceptions;
using ToDoTaskApi.Application.Mappers;
using ToDoTaskApi.Domain.Enums;
using ToDoTaskApi.Domain.Interfaces;

namespace ToDoTaskApi.Application.Managements.Queries.GetIncomingToDoTask
{
    public class GetIncomingToDoTaskHandler(IToDoTaskRepository toDoTaskRepository) : IRequestHandler<GetIncomingToDoTaskRequest, IEnumerable<ToDoTaskDTO>>
    {
        private readonly IToDoTaskRepository _toDoTaskRepository = toDoTaskRepository;

        public async Task<IEnumerable<ToDoTaskDTO>> Handle(GetIncomingToDoTaskRequest request, CancellationToken cancellationToken)
        {
            DateTime startDate = DateTime.UtcNow;
            DateTime endDate = DateTime.UtcNow;

            switch (request.period)
            {
                case ToDoTaskPeriod.Today:
                    endDate = startDate.Date.AddDays(1);
                    break;
                case ToDoTaskPeriod.NextDay:
                    startDate = startDate.Date.AddDays(1); 
                    endDate = startDate.Date.AddDays(1); 
                    break;
                case ToDoTaskPeriod.Week:
                    endDate = startDate.Date.AddDays(8 - (int)startDate.DayOfWeek); 
                    break;
                default:
                    throw new ValidationException("Invalid period");

            }

            var tasks = await _toDoTaskRepository.GetTasksForPeriod(startDate, endDate);

            var mapper = new ToDoTaskMapper();
            return tasks.Select(mapper.ToDoTaskToToDoTaskDto);
        }
    }
}
