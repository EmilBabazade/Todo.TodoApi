namespace Todo.TodoApi.Models;

public class TodoEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Content { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset Deadline { get; set; }
    public bool Completed { get; set; }
}
