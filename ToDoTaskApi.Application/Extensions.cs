using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ToDoTaskApi.Abstractions.Exceptions;

namespace ToDoTaskApi.Application
{
    // Extension method for registering application services (MediatR handlers).
    public static class Extensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<ErrorHandlingMiddleware>();
        }
    }
}
