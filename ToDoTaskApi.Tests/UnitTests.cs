using FluentAssertions;
using ToDoTaskApi.Application.DTO;
using Refit;
using System.Net;
using Xunit;
using ToDoTaskApi.Application.Managements.Commands.CreateToDoTask;
using ToDoTaskApi.Tests;

namespace ToDoTaskApi.Tests
{
    // Unit tests for the ToDoTask API using CustomWebApplicationFactory for in-memory server
    public class UnitTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly IMyNewApi _myNewApi;


        // Constructor sets up the Refit client using the test server's HttpClient
        public UnitTests(CustomWebApplicationFactory factory)
        {
            var client = factory.CreateClient();
            _myNewApi = RestService.For<IMyNewApi>(client);
        }


        [Fact]
        public async Task GetProducts()
        {
            // Act
            var products = await _myNewApi.GetTasks();

            // Assert
            products.Should().NotBeNull();
            products.Should().BeOfType<List<ToDoTaskDTO>>();
        }

        [Fact]
        public async Task CreateProduct()
        {
            // Arrange
            var request = new CreateToDoTaskRequest("Do groceries", "Buy milk, bread and eggs", DateTime.UtcNow.AddHours(4));

            // Act
            var response = await _myNewApi.CreateTask(request);

            // Assert
            response.Content.Should().BeOfType<ToDoTaskDTO>();

        }
    }
}