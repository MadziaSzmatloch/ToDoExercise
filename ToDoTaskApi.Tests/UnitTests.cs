using FluentAssertions;
using ToDoTaskApi.Application.DTO;
using Refit;
using System.Net;
using Xunit;
using ToDoTaskApi.Application.Managements.Commands.CreateToDoTask;
using ToDoTaskApi.Tests;
using ToDoTaskApi.Application.Managements.Commands.UpdateToDoTask;
using ToDoTaskApi.Application.Managements.Commands.SetToDoTaskPercent;
using ToDoTaskApi.Domain.Enums;
using System.Diagnostics;

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

        [Fact]
        public async Task UpdateTask()
        {
            // Arrange
            var createRequest = new CreateToDoTaskRequest("Do groceries", "Buy milk, bread and eggs", DateTime.UtcNow.AddHours(4));
            var created = await _myNewApi.CreateTask(createRequest);
            var request = new UpdateToDoTaskRequest(new ToDoTaskDTO()
            {
                Id = created.Content.Id,
                Title = "New title",
                Description = created.Content.Description,
                ExpirationDate = created.Content.ExpirationDate,
                PercentOfCompletness = created.Content.PercentOfCompletness,
                IsCompleted = created.Content.IsCompleted,
            });


            // Act
            var response = await _myNewApi.UpdateTask(request);

            // Assert
            response.Content.Should().NotBeNull();
            response.Content.Title.Should().Be("New title");
        }

        [Fact]
        public async Task GetTaskById()
        {
            // Arrange
            var createRequest = new CreateToDoTaskRequest("Test task", "Test description", DateTime.UtcNow.AddHours(2));
            var created = await _myNewApi.CreateTask(createRequest);

            // Act
            var response = await _myNewApi.GetTaskById(created.Content.Id);

            // Assert
            response.Content.Should().NotBeNull();
            response.Content.Id.Should().Be(created.Content.Id);
        }

        [Fact]
        public async Task DeleteTask()
        {
            // Arrange
            var createRequest = new CreateToDoTaskRequest("Task to delete", "Delete me", DateTime.UtcNow.AddHours(1));
            var created = await _myNewApi.CreateTask(createRequest);

            // Act
            var response = await _myNewApi.DeleteTask(created.Content.Id);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async Task SetPercentage()
        {
            // Arrange
            Debugger.Launch();
            var createRequest = new CreateToDoTaskRequest("Task with progress", "Progress test", DateTime.UtcNow.AddHours(5));
            var created = await _myNewApi.CreateTask(createRequest);

            var request = new SetToDoTaskPercentRequest(created.Content.Id, 75);

            // Act
            var response = await _myNewApi.SetPercentage(request);

            // Assert
            response.Content.Should().NotBeNull();
            response.Content.PercentOfCompletness.Should().Be(75);
        }


        [Fact]
        public async Task MarkAsDone()
        {
            // Arrange
            var createRequest = new CreateToDoTaskRequest("Task to complete", "Finish this", DateTime.UtcNow.AddHours(2));
            var created = await _myNewApi.CreateTask(createRequest);

            // Act
            var response = await _myNewApi.MarkAsDone(created.Content.Id);

            // Assert
            response.Content.Should().NotBeNull();
            response.Content.IsCompleted.Should().BeTrue();
        }

        [Fact]
        public async Task GetIncomingTasks()
        {
            // Arrange
            var createRequest = new CreateToDoTaskRequest("Incoming task", "Incoming description", DateTime.UtcNow.AddHours(6));
            await _myNewApi.CreateTask(createRequest);

            // Act
            var tasks = await _myNewApi.GetIncomingTasks(ToDoTaskPeriod.Today);

            // Assert
            tasks.Should().NotBeNull();
            tasks.Should().ContainSingle(t => t.Title == "Incoming task");
        }
    }
}