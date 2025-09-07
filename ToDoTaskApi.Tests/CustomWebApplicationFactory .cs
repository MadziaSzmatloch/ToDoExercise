using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToDoTaskApi.Infrastructure;
using Xunit;

namespace ToDoTaskApi.Tests
{

    // Custom WebApplicationFactory that sets up a test environment for the API
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly string _connectionString = "Host=localhost;Port=5444;Database=tododatabase;Username=postgres;Password=postgres";


        // Called before tests run, ensures database is recreated and migrations are applied
        public async Task InitializeAsync()
        {
            var scope = this.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ToDoTaskApiDbContext>();

            await context.Database.EnsureDeletedAsync();
            await context.Database.MigrateAsync();
        }


        // Configure the web host to override default services for testing
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<ToDoTaskApiDbContext>>();

                services.AddDbContext<ToDoTaskApiDbContext>(options => options.UseNpgsql(_connectionString));

            });

            builder.UseEnvironment("Development");
        }

        // Cleanup after tests
        Task IAsyncLifetime.DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }

}
