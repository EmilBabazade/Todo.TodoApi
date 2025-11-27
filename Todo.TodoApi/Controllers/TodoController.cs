using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.TodoApi.Models;

namespace Todo.TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController(TodoRepo todoRepo) : ControllerBase
{
    private readonly TodoRepo _todoRepo = todoRepo;

    // get many
    [HttpGet]
    public async Task<IEnumerable<TodoEntity>> GetTodosAsync(CancellationToken cancellationToken)
    {
        return await _todoRepo.GetTodosAsync(cancellationToken);
    }

    // get one
    [HttpGet("{id}")]
    public async Task<TodoEntity?> GetTodoAsync(int id, CancellationToken cancellationToken)
    {
        return await _todoRepo.GetTodoAsync(id, cancellationToken);
    }

    // insert one

    // update one

    // delete one
}
