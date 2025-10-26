namespace Tasks.Presentation.DTOs
{
    public record TaskRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }

}

