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

        public Task Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ToDoTask>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ToDoTask> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task Update(ToDoTask toDoTask)
        {
            throw new NotImplementedException();
        }
    }
}
