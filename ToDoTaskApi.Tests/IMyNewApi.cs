using ToDoTaskApi.Application.DTO;
using Refit;
using ToDoTaskApi.Application.Managements.Commands.CreateToDoTask;
using ToDoTaskApi.Application.Managements.Queries.GetToDoTaskById;
using ToDoTaskApi.Application.Managements.Commands.UpdateToDoTask;
using ToDoTaskApi.Application.Managements.Commands.DeleteToDoTask;
using ToDoTaskApi.Application.Managements.Commands.SetToDoTaskPercent;
using ToDoTaskApi.Application.Managements.Commands.MarkToDoTaskAsDone;
using ToDoTaskApi.Application.Managements.Queries.GetIncomingToDoTask;
using ToDoTaskApi.Domain.Enums;

namespace ToDoTaskApi.Tests
{
    // Defines a typed client interface for calling the ToDoTask API using Refit
    public interface IMyNewApi
    {
        [Get("/ToDoTasks")]
        Task<IEnumerable<ToDoTaskDTO>> GetTasks();

        [Post("/ToDoTasks")]
        Task<ApiResponse<ToDoTaskDTO>> CreateTask([Body] CreateToDoTaskRequest request);


        [Put("/ToDoTasks")]
        Task<ApiResponse<ToDoTaskDTO>> UpdateTask([Body] UpdateToDoTaskRequest request);


        [Get("/ToDoTasks/{id}")]
        Task<ApiResponse<ToDoTaskDTO>> GetTaskById(Guid id);


        [Delete("/ToDoTasks/{id}")]
        Task<IApiResponse> DeleteTask(Guid id);


        [Patch("/ToDoTasks/setPercent")]
        Task<ApiResponse<ToDoTaskDTO>> SetPercentage([Body] SetToDoTaskPercentRequest request);


        [Patch("/ToDoTasks/markAsDone/{id}")]
        Task<ApiResponse<ToDoTaskDTO>> MarkAsDone(Guid id);


        [Get("/ToDoTasks/incoming")]
        Task<IEnumerable<ToDoTaskDTO>> GetIncomingTasks(ToDoTaskPeriod period);
    }
}
