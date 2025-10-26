using Moq;
using Tasks.Application.Usecases;
using Tasks.Domain.Repositories;
using Tasks.Application.DTOs;
using Xunit;

namespace Tasks.Tests.Application.Usecases
{
    public class TaskApplicationUsecasesTests
    {
        [Fact]
        public async Task AddTask_Should_Create_A_Task()
        {
            var mockRepo = new Mock<ITaskRepository>();

            mockRepo.Setup(r => r.ExistsByTitleAsync(It.IsAny<string>()))
                    .ReturnsAsync(false);

            var addTaskUsecase = new AddTask(mockRepo.Object);

            var request = new TaskRequestDTO
            {
                Title = "New Task",
                Description = "Description of the Task"
            };

            var result = await addTaskUsecase.ExecuteAsync(request);

            Assert.NotNull(result);
            Assert.Equal(request.Title, result.Title);
            Assert.Equal(request.Description, result.Description);

            mockRepo.Verify(r => r.AddAsync(It.IsAny<Tasks.Domain.Entities.Task>()), Times.Once);
        }
    }
}
