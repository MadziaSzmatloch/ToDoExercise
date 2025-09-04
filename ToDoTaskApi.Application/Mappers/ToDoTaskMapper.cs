using Riok.Mapperly.Abstractions;
using ToDoTaskApi.Domain.Entities;

namespace ToDoTaskApi.Application.Mappers
{
    [Mapper]
    public partial class ToDoTaskMapper
    {
        public partial DTO.ToDoTaskDTO ToDoTaskToToDoTaskDto(ToDoTask toDoTask);
        public partial ToDoTask ToDoTaskDtoToToDoTask(DTO.ToDoTaskDTO toDoTaskDto);
    }
}
