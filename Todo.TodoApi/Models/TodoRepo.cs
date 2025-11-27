using Dapper;
using MySqlConnector;

namespace Todo.TodoApi.Models;

public class TodoRepo(Func<MySqlConnection> connFactory)
{
    private readonly Func<MySqlConnection> _connFactory = connFactory;

    public async Task<IEnumerable<TodoEntity>> GetTodosAsync(CancellationToken cancellationToken)
    {
        using var conn = _connFactory();

        var todos = await conn.QueryAsync<TodoEntity>(
            "SELECT * FROM Todos",
            cancellationToken
            );

        return todos;
    }

    public async Task<TodoEntity?> GetTodoAsync(int id, CancellationToken cancellationToken)
    {
        using var conn = _connFactory();

        var command = new CommandDefinition("SELECT * FROM Todos WHERE id = @id", new { id }, cancellationToken: cancellationToken);
        return await conn.QueryFirstAsync<TodoEntity?>(command);
    }
}
