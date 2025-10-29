using Moq;
using Tasks.Application.Usecases;
using Tasks.Domain.Repositories;
using Tasks.Domain.Services;
using Xunit;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Tests.Application.Usecases
{
    public class DeleteTaskUseCaseTest
    {
        [Fact]
        public async Task DeleteTask_Should_Return_True()
        {
            var mockRepo = new Mock<ITaskRepository>();
            var mockService = new Mock<TaskDomainService>();

            var task = new DomainTask("Test Task", "Some Description");
            Guid taskId = task.Id;

            mockRepo.Setup(r => r.GetByIdAsync(taskId))
                    .ReturnsAsync(task);

            var useCase = new DeleteTaskUseCase(mockRepo.Object, mockService.Object);

            var result = await useCase.ExecuteAsync(taskId);

            Assert.True(result);
            mockRepo.Verify(r => r.DeleteAsync(task), Times.Once);
        }
    }
}
