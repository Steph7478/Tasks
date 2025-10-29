using Microsoft.AspNetCore.Mvc;
using Moq;
using Tasks.Application.DTOs;
using Tasks.Application.Repositories;
using Tasks.Domain.Enums;
using Tasks.Presentation.Controllers;
using Tasks.Presentation.DTOs;
using Xunit;

namespace Tasks.Tests.Presentation.Controllers
{
    public class TasksController_CreateTask_Tests
    {
        private readonly Mock<IAddTask> _mockAddTaskUseCase;
        private readonly TasksController _controller;

        public TasksController_CreateTask_Tests()
        {
            _mockAddTaskUseCase = new Mock<IAddTask>();

            _controller = new TasksController(
                addTaskUseCase: _mockAddTaskUseCase.Object,
                getTaskByIdUseCase: new Mock<IGetTaskById>().Object,
                updateTaskUseCase: new Mock<IUpdateTask>().Object,
                completeTask: new Mock<IUpdateStatus>().Object,
                deleteTaskUseCase: new Mock<IDeleteTask>().Object,
                getAllTasksUseCase: new Mock<IGetAllTasks>().Object
            );
        }

        [Fact]
        public async Task CreateTask_ReturnsOk()
        {
            var request = new TaskRequest { Title = "New Task", Description = "Test" };

            var responseDto = new TaskResponseDTO
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                CurrentStatus = Status.Pending
            };

            _mockAddTaskUseCase
                .Setup(x => x.ExecuteAsync(It.IsAny<TaskRequestDTO>()))
                .ReturnsAsync(responseDto);

            var result = await _controller.CreateTask(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<TaskResponse>(okResult.Value);

            Assert.Equal(request.Title, response.Title);

            _mockAddTaskUseCase.Verify(x => x.ExecuteAsync(It.IsAny<TaskRequestDTO>()), Times.Once);
        }
    }
}
