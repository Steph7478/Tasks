using Moq;
using Tasks.Application.Usecases;
using Tasks.Domain.Repositories;
using Xunit;
using DomainTask = Tasks.Domain.Entities.Task;

namespace Tasks.Tests.Application.Usecases
{
    public class GetAllTasksTest
    {
        [Fact]
        public async Task GetAllTasks_Should_Return_All()
        {
            var mockRepo = new Mock<ITaskRepository>();
            var listOfTasks = new List<DomainTask>
            {
                new("Test 1", "Study C#"),
                new("Test 2", "Learn EF Core")
            };

            mockRepo.Setup(r => r.GetAllAsync())
                    .ReturnsAsync(listOfTasks);

            var useCase = new GetAllTasksUseCase(mockRepo.Object);

            var result = await useCase.ExecuteAsync();
            Assert.Equal(listOfTasks.Count, result.Count());

            for (int i = 0; i < listOfTasks.Count; i++)
            {
                var expected = listOfTasks[i];
                var actual = result.ElementAt(i);
                Assert.Equal(expected.Title, actual.Title);
                Assert.Equal(expected.Description, actual.Description);
            }
        }
    }
}
