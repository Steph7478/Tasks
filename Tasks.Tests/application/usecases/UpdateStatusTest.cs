using Moq;
using Tasks.Application.Usecases;
using Tasks.Domain.Enums;
using Tasks.Domain.Repositories;
using Tasks.Domain.Services;
using Xunit;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Tests.Application.Usecases
{
    public class UpdateStatusTest
    {
        [Fact]
        public async Task UpdateStatus_Should_Be_Completed()
        {
            var mockRepo = new Mock<ITaskRepository>();
            var mockService = new Mock<TaskDomainService>();
            var existingTask = new DomainTask("Title", "Description");

            mockRepo
                .Setup(r => r.GetByIdAsync(existingTask.Id))
                .ReturnsAsync(existingTask);

            mockRepo
                .Setup(r => r.UpdateAsync(It.IsAny<DomainTask>()))
                .Returns(Task.CompletedTask);

            var useCase = new UpdateStatusUseCase(mockRepo.Object, mockService.Object);

            await useCase.ExecuteAsync(existingTask.Id, Status.Completed);

            Assert.Equal(Status.Completed, existingTask.CurrentStatus);

            mockRepo.Verify(r => r.UpdateAsync(It.Is<DomainTask>(t => t.CurrentStatus == Status.Completed)), Times.Once);
        }
    }
}
