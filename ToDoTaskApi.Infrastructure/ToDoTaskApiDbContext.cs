using Microsoft.EntityFrameworkCore;
using ToDoTaskApi.Domain.Entities;

namespace ToDoTaskApi.Infrastructure
{
    // EF Core DbContext representing the database of the application.
    public class ToDoTaskApiDbContext : DbContext
    {
        // Constructor with options passed from DI
        public ToDoTaskApiDbContext(DbContextOptions options)
            : base(options)
        { }


        // Represents the ToDoTasks table in the database.
        public DbSet<ToDoTask> ToDoTasks { get; set; }
    }
}
