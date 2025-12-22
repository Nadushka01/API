namespace TodoApi.DTOs
{
    public class TodoItemCreateDto
    {
        public string Title { get; set; } = "";

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}