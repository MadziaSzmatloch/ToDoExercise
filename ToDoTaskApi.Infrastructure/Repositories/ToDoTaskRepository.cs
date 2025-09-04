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
    internal class ToDoTaskRepository(ToDoTaskApiDbContext doTaskApiDbContext) : IToDoTaskRepository
    {
        private readonly ToDoTaskApiDbContext _doTaskApiDbContext = doTaskApiDbContext;
        private DbSet<ToDoTask> _toDoTasks = doTaskApiDbContext.ToDoTasks;

        public async Task Add(ToDoTask toDoTask)
        {
            await _toDoTasks.AddAsync(toDoTask);
            await _doTaskApiDbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var forDelete = await _toDoTasks.FirstAsync(x => x.Id == id);
            _toDoTasks.Remove(forDelete);
            await _doTaskApiDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoTask>> GetAll()
        {
            return await _toDoTasks.ToListAsync();
        }

        public async Task<ToDoTask> GetById(Guid id)
        {
            return await _toDoTasks.FirstAsync(x => x.Id == id);
        }

        public async Task Update(ToDoTask toDoTask)
        {
            var forUpdate = await _toDoTasks.FirstAsync(p => p.Id == toDoTask.Id);
            forUpdate.Title = toDoTask.Title;
            forUpdate.Description = toDoTask.Description;
            forUpdate.ExpirationDate = toDoTask.ExpirationDate;
            forUpdate.PercentOfCompletness = toDoTask.PercentOfCompletness;
            forUpdate.IsCompleted = toDoTask.IsCompleted;

            await _doTaskApiDbContext.SaveChangesAsync();
        }
    }
}
