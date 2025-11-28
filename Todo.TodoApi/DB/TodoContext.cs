using Microsoft.EntityFrameworkCore;
using Todo.TodoApi.Models;

namespace Todo.TodoApi.DB;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions options) : base(options)
    {
    }

    protected TodoContext()
    {
    }

    public DbSet<TodoEntity> Todos { get; set; }
}
