using Microsoft.EntityFrameworkCore;
using ToDoTaskApi.Domain.Entities;

namespace ToDoTaskApi.Infrastructure
{
    public class ToDoTaskApiDbContext : DbContext
    {
        public ToDoTaskApiDbContext()
        {

        }

        public ToDoTaskApiDbContext(DbContextOptions options)
            : base(options)
        { }


        public DbSet<ToDoTask> ToDoTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
