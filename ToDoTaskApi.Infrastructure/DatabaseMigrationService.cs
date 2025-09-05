using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ToDoTaskApi.Infrastructure
{
    // Hosted service that applies any pending EF Core database migrations at application startup.
    // Ensures the database schema is up-to-date without manual intervention.
    public class DatabaseMigrationService(IServiceProvider serviceProvider) : IHostedService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        // Triggered when the application starts.
        // Creates a scoped DbContext and applies pending migrations if any exist.
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ToDoTaskApiDbContext>();

            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
                await dbContext.Database.MigrateAsync(cancellationToken);
        }

        // Triggered when the application stops.
        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}