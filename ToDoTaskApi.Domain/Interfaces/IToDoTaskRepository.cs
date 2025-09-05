using ToDoTaskApi.Domain.Entities;

namespace ToDoTaskApi.Domain.Interfaces
{
    // IRepository defines the contract for data access operations. 
    // It abstracts the persistence logic so that higher layers 
    //  don’t depend on the database directly.
    public interface IToDoTaskRepository
    {
        public Task<IEnumerable<ToDoTask>> GetAll();
        public Task<ToDoTask?> GetById(Guid id);
        public Task<IEnumerable<ToDoTask>> GetTasksForPeriod(DateTime startDate, DateTime endDate);
        public Task Add(ToDoTask toDoTask);
        public Task<bool> Update(ToDoTask toDoTask);
        public Task<bool> Delete(Guid id);
    }
}
