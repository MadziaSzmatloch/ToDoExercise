using Riok.Mapperly.Abstractions;
using ToDoTaskApi.Domain.Entities;

namespace ToDoTaskApi.Application.Mappers
{
    // This class is an example of a Mapper.
    // I created it to demonstrate that I understand the purpose of mappers 
    // and how to use them. I also know that writing custom mapping logic 
    // can sometimes be faster, but I chose to use this approach because 
    // it provides good performance results while keeping the code cleaner 
    // and easier to maintain.
    [Mapper]
    public partial class ToDoTaskMapper
    {
        public partial DTO.ToDoTaskDTO ToDoTaskToToDoTaskDto(ToDoTask toDoTask);
        public partial ToDoTask ToDoTaskDtoToToDoTask(DTO.ToDoTaskDTO toDoTaskDto);
    }
}
