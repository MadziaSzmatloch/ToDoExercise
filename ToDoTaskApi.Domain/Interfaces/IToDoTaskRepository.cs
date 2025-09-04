using ToDoTaskApi.Domain.Entities;

namespace ToDoTaskApi.Domain.Interfaces
{
    public interface IToDoTaskRepository
    {
        public Task<IEnumerable<ToDoTask>> GetAll();
        public Task<ToDoTask> GetById(Guid Id);
        public Task Add(ToDoTask toDoTask);
        public Task Update(ToDoTask toDoTask);
        public Task Delete(Guid Id);
    }
}
