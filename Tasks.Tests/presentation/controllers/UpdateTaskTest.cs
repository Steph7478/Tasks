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
    public class TasksController_UpdateTask_Tests
    {
        private readonly Mock<IUpdateTask> _mockUpdateTask;
        private readonly TasksController _controller;

        public TasksController_UpdateTask_Tests()
        {
            _mockUpdateTask = new Mock<IUpdateTask>();

            _controller = new TasksController(
                addTaskUseCase: new Mock<IAddTask>().Object,
                getTaskByIdUseCase: new Mock<IGetTaskById>().Object,
                updateTaskUseCase: _mockUpdateTask.Object,
                completeTask: new Mock<IUpdateStatus>().Object,
                deleteTaskUseCase: new Mock<IDeleteTask>().Object,
                getAllTasksUseCase: new Mock<IGetAllTasks>().Object
            );
        }

        [Fact]
        public async Task UpdateTask_ReturnsOkWithUpdatedTask()
        {
            var id = Guid.NewGuid();
            var request = new TaskRequest { Title = "Updated Title", Description = "Updated Desc" };

            var updatedDto = new TaskResponseDTO
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                CurrentStatus = Status.InProgress,
                CreatedAt = DateTime.UtcNow
            };

            _mockUpdateTask.Setup(x => x.ExecuteAsync(id, It.IsAny<TaskRequestDTO>()))
                .ReturnsAsync(updatedDto);

            var result = await _controller.UpdateTask(id, request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<TaskResponse>(okResult.Value);

            Assert.Equal("Updated Title", response.Title);

            _mockUpdateTask.Verify(x => x.ExecuteAsync(id, It.IsAny<TaskRequestDTO>()), Times.Once);
        }
    }
}
