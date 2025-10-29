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
    public class TasksController_GetAllTasks_Tests
    {
        private readonly Mock<IGetAllTasks> _mockGetAllTasks;
        private readonly TasksController _controller;

        public TasksController_GetAllTasks_Tests()
        {
            _mockGetAllTasks = new Mock<IGetAllTasks>();

            _controller = new TasksController(
                addTaskUseCase: new Mock<IAddTask>().Object,
                getTaskByIdUseCase: new Mock<IGetTaskById>().Object,
                updateTaskUseCase: new Mock<IUpdateTask>().Object,
                completeTask: new Mock<IUpdateStatus>().Object,
                deleteTaskUseCase: new Mock<IDeleteTask>().Object,
                getAllTasksUseCase: _mockGetAllTasks.Object
            );
        }

        [Fact]
        public async Task GetAllTasks_ReturnsOkWithList()
        {
            var tasks = new List<TaskResponseDTO>
            {
                new() { Id = Guid.NewGuid(), Title = "Task 1", Description = "Desc 1", CreatedAt = DateTime.UtcNow, CurrentStatus = Status.Pending },
                new() { Id = Guid.NewGuid(), Title = "Task 2", Description = "Desc 2", CreatedAt = DateTime.UtcNow, CurrentStatus = Status.Completed }
            };

            _mockGetAllTasks.Setup(x => x.ExecuteAsync()).ReturnsAsync(tasks);

            var result = await _controller.GetAllTasks();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<IEnumerable<TaskResponse>>(okResult.Value, exactMatch: false);
            Assert.Equal(2, response.Count());

            _mockGetAllTasks.Verify(x => x.ExecuteAsync(), Times.Once);
        }
    }
}
