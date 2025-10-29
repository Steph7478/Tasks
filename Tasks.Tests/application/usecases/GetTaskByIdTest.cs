using Moq;
using Tasks.Application.Usecases;
using Tasks.Domain.Repositories;
using Xunit;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Tests.Application.Usecases
{
    public class GetTaskByIdTest
    {
        [Fact]
        public async Task GetTaskById_Should_Return_Entity()
        {
            var mockRepo = new Mock<ITaskRepository>();
            var existingTask = new DomainTask("Title", "Description");

            mockRepo.Setup(r => r.GetByIdAsync(existingTask.Id))
                    .ReturnsAsync(existingTask);

            var useCase = new GetTaskById(mockRepo.Object);

            var result = await useCase.ExecuteAsync(existingTask.Id);

            Assert.Equal(existingTask.Id, result.Id);
            Assert.Equal(existingTask.Title, result.Title);
            Assert.Equal(existingTask.Description, result.Description);
        }
    }
}
