using FluentAssertions;
using Moq;
using ToDoTaskApi.Application.Managements.Commands.CreateToDoTask;
using ToDoTaskApi.Application.Managements.Commands.DeleteToDoTask;
using ToDoTaskApi.Application.Managements.Commands.MarkToDoTaskAsDone;
using ToDoTaskApi.Application.Managements.Commands.UpdateToDoTask;
using ToDoTaskApi.Application.Managements.Queries.GetAllToDoTask;
using ToDoTaskApi.Application.Managements.Queries.GetToDoTaskById;
using ToDoTaskApi.Application.Mappers;
using ToDoTaskApi.Domain.Entities;
using ToDoTaskApi.Domain.Interfaces;
using Xunit;

namespace ToDoTaskApi.Tests
{
    public class UnitTests
    {
        [Fact]
        public async Task TestCreateToDoTaskHandler()
        {
            // Arrange
            var mockRepo = new Mock<IToDoTaskRepository>();

            mockRepo.Setup(r => r.Add(It.IsAny<ToDoTask>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateToDoTaskHandler(mockRepo.Object);

            var command = new CreateToDoTaskRequest("Title", "Desc", DateTime.UtcNow.AddDays(1));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Title");
            mockRepo.Verify(r => r.Add(It.IsAny<ToDoTask>()), Times.Once);
        }

        [Fact]
        public async Task TestGetAllTasksHandler()
        {
            // Arrange
            var mockRepo = new Mock<IToDoTaskRepository>();

            var tasks = new List<ToDoTask>
            {
                new ToDoTask("Task1", "Desc1", DateTime.UtcNow.AddHours(2)),
                new ToDoTask("Task2", "Desc2", DateTime.UtcNow.AddHours(2))
            };

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(tasks);

            var handler = new GetAllToDoTasksHandler(mockRepo.Object);

            var command = new GetAllToDoTasksRequest();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            mockRepo.Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public async Task TestGetTaskByIdHandler()
        {
            // Arrange
            var mockRepo = new Mock<IToDoTaskRepository>();

            var task = new ToDoTask("Task1", "Desc1", DateTime.UtcNow.AddHours(2));

            mockRepo.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync(task);

            var handler = new GetToDoTaskByIdHandler(mockRepo.Object);

            var command = new GetToDoTaskByIdRequest(task.Id);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Task1");
            mockRepo.Verify(r => r.GetById(task.Id), Times.Once);
        }

        [Fact]
        public async Task TestDeleteTaskHandler()
        {
            // Arrange
            var mockRepo = new Mock<IToDoTaskRepository>();

            var taskId = Guid.NewGuid();

            mockRepo.Setup(r => r.Delete(taskId)).ReturnsAsync(true);

            var handler = new DeleteToDoTaskHandler(mockRepo.Object);

            var command = new DeleteToDoTaskRequest(taskId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            mockRepo.Verify(r => r.Delete(taskId), Times.Once);
        }

        [Fact]
        public async Task TestUpdateTaskHandler()
        {
            // Arrange
            var mockRepo = new Mock<IToDoTaskRepository>();

            var task = new ToDoTask("Task1", "Desc1", DateTime.UtcNow.AddHours(2));

            mockRepo.Setup(r => r.Update(It.IsAny<ToDoTask>())).ReturnsAsync(true);

            var handler = new UpdateToDoTaskHandler(mockRepo.Object);
            var mapper = new ToDoTaskMapper();
            var command = new UpdateToDoTaskRequest(mapper.ToDoTaskToToDoTaskDto(task));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be("Task1");
            mockRepo.Verify(r => r.Update(It.IsAny<ToDoTask>()), Times.Once);
        }

        [Fact]
        public async Task TestMarkTaskAsDoneHandler()
        {
            // Arrange
            var mockRepo = new Mock<IToDoTaskRepository>();

            var task = new ToDoTask("Task1", "Desc1", DateTime.UtcNow.AddHours(2));

            mockRepo.Setup(r => r.GetById(task.Id)).ReturnsAsync(task);
            mockRepo.Setup(r => r.Update(It.IsAny<ToDoTask>())).ReturnsAsync(true);

            var handler = new MarkToDoTaskAsDoneHandler(mockRepo.Object);
            var command = new MarkToDoTaskAsDoneRequest(task.Id);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsCompleted.Should().BeTrue();
            mockRepo.Verify(r => r.GetById(task.Id), Times.Once);
            mockRepo.Verify(r => r.Update(It.IsAny<ToDoTask>()), Times.Once);
        }
    }
}
