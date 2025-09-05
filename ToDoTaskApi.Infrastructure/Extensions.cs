using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoTaskApi.Domain.Interfaces;
using ToDoTaskApi.Infrastructure.Repositories;

namespace ToDoTaskApi.Infrastructure
{
    // Extension method for registering infrastructure layer services in the DI container (DbContext, migrations, repositories).
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToDoTaskApiDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Database")));
            services.AddHostedService<DatabaseMigrationService>();

            services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();

            return services;
        }
    }
}
