using Microsoft.AspNetCore.Mvc;
using Moq;
using Tasks.Application.Repositories;
using Tasks.Presentation.Controllers;
using Xunit;

namespace Tasks.Tests.Presentation.Controllers
{
    public class TasksController_DeleteTask_Tests
    {
        private readonly Mock<IDeleteTask> _mockDeleteTask;
        private readonly TasksController _controller;

        public TasksController_DeleteTask_Tests()
        {
            _mockDeleteTask = new Mock<IDeleteTask>();

            _controller = new TasksController(
                addTaskUseCase: new Mock<IAddTask>().Object,
                getTaskByIdUseCase: new Mock<IGetTaskById>().Object,
                updateTaskUseCase: new Mock<IUpdateTask>().Object,
                completeTask: new Mock<IUpdateStatus>().Object,
                deleteTaskUseCase: _mockDeleteTask.Object,
                getAllTasksUseCase: new Mock<IGetAllTasks>().Object
            );
        }

        [Fact]
        public async Task DeleteTask_Existing_ReturnsNoContent()
        {
            var id = Guid.NewGuid();
            _mockDeleteTask.Setup(x => x.ExecuteAsync(id)).ReturnsAsync(true);

            var result = await _controller.DeleteTask(id);

            Assert.IsType<NoContentResult>(result);
            _mockDeleteTask.Verify(x => x.ExecuteAsync(id), Times.Once);
        }
    }
}
