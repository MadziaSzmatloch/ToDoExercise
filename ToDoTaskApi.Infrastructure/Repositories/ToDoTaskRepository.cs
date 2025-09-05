using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoTaskApi.Domain.Entities;
using ToDoTaskApi.Domain.Interfaces;

namespace ToDoTaskApi.Infrastructure.Repositories
{
    // Repository is a concrete implementation of IRepository. 
    // It contains the actual logic for interacting with the database.
    // This ensures clean separation between the domain logic and data access layer.
    internal class ToDoTaskRepository(ToDoTaskApiDbContext doTaskApiDbContext) : IToDoTaskRepository
    {
        private readonly ToDoTaskApiDbContext _doTaskApiDbContext = doTaskApiDbContext;
        private DbSet<ToDoTask> _toDoTasks = doTaskApiDbContext.ToDoTasks;

        public async Task Add(ToDoTask toDoTask)
        {
            await _toDoTasks.AddAsync(toDoTask);
            await _doTaskApiDbContext.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var forDelete = await _toDoTasks.FirstAsync(x => x.Id == id);
            if (forDelete is null)
                return false;
            _toDoTasks.Remove(forDelete);
            await _doTaskApiDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ToDoTask>> GetAll()
        {
            return await _toDoTasks.ToListAsync();
        }

        public async Task<ToDoTask?> GetById(Guid id)
        {
            return await _toDoTasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(ToDoTask toDoTask)
        {
            var forUpdate = await _toDoTasks.FirstOrDefaultAsync(p => p.Id == toDoTask.Id);
            if (forUpdate is null)
                return false;

            forUpdate.Title = toDoTask.Title;
            forUpdate.Description = toDoTask.Description;
            forUpdate.ExpirationDate = toDoTask.ExpirationDate;
            forUpdate.PercentOfCompletness = toDoTask.PercentOfCompletness;
            forUpdate.IsCompleted = toDoTask.IsCompleted;

            await _doTaskApiDbContext.SaveChangesAsync();
            return true;
        }
    }
}
