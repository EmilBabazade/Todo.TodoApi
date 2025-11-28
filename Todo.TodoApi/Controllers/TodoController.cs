using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.TodoApi.Models;

namespace Todo.TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController(Repo<TodoEntity> todoRepo) : ControllerBase
{
    private readonly Repo<TodoEntity> _todoRepo = todoRepo;

    // get many
    [HttpGet]
    public async Task<IEnumerable<TodoEntity>> GetTodosAsync(CancellationToken cancellationToken)
    {
        return await _todoRepo.GetManyAsync(cancellationToken: cancellationToken);
    }

    // get one
    [HttpGet("{id}")]
    public async Task<TodoEntity?> GetTodoAsync(int id, CancellationToken cancellationToken)
    {
        return await _todoRepo.GetOneAsync(x => x.Id == id, cancellationToken);
    }

    // insert one

    // update one

    // delete one
}
