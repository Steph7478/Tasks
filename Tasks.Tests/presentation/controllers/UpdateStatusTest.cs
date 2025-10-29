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
    public class TasksController_UpdateStatus_Tests
    {
        private readonly Mock<IUpdateStatus> _mockUpdateStatus;
        private readonly TasksController _controller;

        public TasksController_UpdateStatus_Tests()
        {
            _mockUpdateStatus = new Mock<IUpdateStatus>();

            _controller = new TasksController(
                addTaskUseCase: new Mock<IAddTask>().Object,
                getTaskByIdUseCase: new Mock<IGetTaskById>().Object,
                updateTaskUseCase: new Mock<IUpdateTask>().Object,
                completeTask: _mockUpdateStatus.Object,
                deleteTaskUseCase: new Mock<IDeleteTask>().Object,
                getAllTasksUseCase: new Mock<IGetAllTasks>().Object
            );
        }

        [Fact]
        public async Task UpdateTaskStatus_ReturnsOkWithUpdatedTask()
        {
            var id = Guid.NewGuid();
            var request = new UpdateStatusRequest { CurrentStatus = Status.Completed };

            var responseDto = new TaskResponseDTO
            {
                Id = id,
                Title = "My Task",
                Description = "Updated",
                CreatedAt = DateTime.UtcNow,
                CurrentStatus = Status.Completed
            };

            _mockUpdateStatus
                .Setup(x => x.ExecuteAsync(id, request.CurrentStatus))
                .ReturnsAsync(responseDto);

            var result = await _controller.UpdateTaskStatus(id, request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<TaskResponse>(okResult.Value);

            Assert.Equal(Status.Completed, response.CurrentStatus);

            _mockUpdateStatus.Verify(x => x.ExecuteAsync(id, Status.Completed), Times.Once);
        }
    }
}
