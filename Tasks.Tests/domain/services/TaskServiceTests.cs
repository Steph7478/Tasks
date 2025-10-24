using Tasks.Domain.Enums;
using Tasks.Domain.Services;
using DomainTask = Tasks.Domain.Entities.Task;
using Xunit;

namespace Tasks.Tests.Domain.Services
{
    public class TaskDomainServiceTests
    {
        [Fact]
        public void ChangeStatus_Should_Update_Status_When_Not_Completed()
        {
            var task = new DomainTask("Test1", "Test2");
            var service = new TaskDomainService();

            service.ChangeStatus(task, Status.InProgress);

            Assert.Equal(Status.InProgress, task.CurrentStatus);
        }

        [Fact]
        public void ChangeStatus_Should_Throw_When_Task_Completed()
        {
            var task = new DomainTask("Test Completed", "Test Working");
            task.UpdateStatus(Status.Completed);

            var service = new TaskDomainService();

            var exception = Assert.Throws<InvalidOperationException>(() =>
                service.ChangeStatus(task, Status.InProgress)
            );

            Assert.Equal("Cannot change status after completion.", exception.Message);
        }
    }
}
