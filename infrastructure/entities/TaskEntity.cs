using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tasks.Domain.Enums;

namespace Tasks.Infrastructure.Entities
{
    [Table("Tasks")]
    public class TaskEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public required string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        public Status CurrentStatus { get; set; }
    }
}
