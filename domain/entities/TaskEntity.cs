using Tasks.Domain.Enums;

namespace Tasks.Domain.Entities
{
    public class Task
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Status CurrentStatus { get; private set; }

        public Task(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.", nameof(description));

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            CurrentStatus = Status.Pending;
        }

        public void UpdateStatus(Status newStatus)
        {
            CurrentStatus = newStatus;
        }
    }
}
