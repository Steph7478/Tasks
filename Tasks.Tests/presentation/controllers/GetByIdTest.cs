using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Tasks.Presentation.Controllers;
using Tasks.Application.DTOs;
using Tasks.Presentation.DTOs;
using Tasks.Domain.Enums;
using Tasks.Application.Repositories;

namespace Tasks.Tests.Presentation.Controllers
{
    public class TasksControllerTests
    {
        private readonly Mock<IGetTaskById> _mockGetTaskByIdUseCase;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            var mockAddTask = new Mock<IAddTask>();
            var mockUpdateTask = new Mock<IUpdateTask>();
            var mockUpdateStatus = new Mock<IUpdateStatus>();
            var mockDeleteTask = new Mock<IDeleteTask>();
            var mockGetAllTasks = new Mock<IGetAllTasks>();

            _mockGetTaskByIdUseCase = new Mock<IGetTaskById>();

            _controller = new TasksController(
                mockAddTask.Object,
                _mockGetTaskByIdUseCase.Object,
                mockUpdateTask.Object,
                mockUpdateStatus.Object,
                mockDeleteTask.Object,
                mockGetAllTasks.Object
            );
        }

        [Fact]
        public async Task GetTaskById_ExistingId_ReturnsOkWithTask()
        {
            var taskId = Guid.NewGuid();

            var applicationResponseDTO = new TaskResponseDTO
            {
                Id = taskId,
                Title = "Test Task",
                Description = "Testing the controller",
                CurrentStatus = Status.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _mockGetTaskByIdUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(applicationResponseDTO);

            var result = await _controller.GetTaskById(taskId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var taskResponse = Assert.IsType<TaskResponse>(okResult.Value);

            Assert.Equal(taskId, taskResponse.Id);

            _mockGetTaskByIdUseCase.Verify(uc => uc.ExecuteAsync(taskId), Times.Once);
        }
    }
}
