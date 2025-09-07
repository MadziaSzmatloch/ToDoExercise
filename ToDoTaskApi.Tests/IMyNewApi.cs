using ToDoTaskApi.Application.DTO;
using Refit;
using ToDoTaskApi.Application.Managements.Commands.CreateToDoTask;

namespace ToDoTaskApi.Tests
{
    public interface IMyNewApi
    {
        [Get("/ToDoTasks")]
        Task<IEnumerable<ToDoTaskDTO>> GetTasks();

        [Post("/ToDoTasks")]
        Task<ApiResponse<ToDoTaskDTO>> CreateTask([Body] CreateToDoTaskRequest request);

    }
}
