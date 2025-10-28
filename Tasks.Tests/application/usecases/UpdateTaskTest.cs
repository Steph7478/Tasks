using Moq;
using Tasks.Application.DTOs;
using Tasks.Application.Usecases;
using Tasks.Domain.Repositories;
using Tasks.Domain.Services;
using Xunit;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Tests.Application.Usecases
{
    public class UpdateTaskUseCaseTest
    {
        [Fact]
        public async Task UpdateTask_Should_Be_Updated()
        {
            var mockRepo = new Mock<ITaskRepository>();
            var mockService = new Mock<TaskDomainService>();

            var existingTask = new DomainTask("Old Title", "Old Description");

            mockRepo.Setup(r => r.GetByIdAsync(existingTask.Id))
                    .ReturnsAsync(existingTask);

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<DomainTask>()))
                    .Returns(Task.CompletedTask);

            var useCase = new UpdateTaskUseCase(mockRepo.Object, mockService.Object);

            var updateRequest = new TaskRequestDTO
            {
                Title = "Updated Title",
                Description = "Updated Description"
            };

            var result = await useCase.ExecuteAsync(existingTask.Id, updateRequest);

            Assert.NotNull(result);
            Assert.Equal(updateRequest.Title, result.Title);
            Assert.Equal(updateRequest.Description, result.Description);

            mockRepo.Verify(r => r.GetByIdAsync(existingTask.Id), Times.Once);
            Assert.Equal(updateRequest.Title, result.Title);
            Assert.Equal(updateRequest.Description, result.Description);
            mockRepo.Verify(r => r.UpdateAsync(existingTask), Times.Once);
        }
    }
}
