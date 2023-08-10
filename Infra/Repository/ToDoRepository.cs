using Microsoft.EntityFrameworkCore;
using ToDoRedis.Models;

namespace ToDoRedis.Infra.Repository;

public class ToDoRepository : ITodoRepository
{
    private readonly DataContext _context;

    public ToDoRepository(DataContext context)
    {
        _context = context;
    }

    public async Task InserirTodos(IEnumerable<ToDo> todos)
    {
        await _context.Todos.AddRangeAsync(todos);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ToDo>> ObterToDos()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        var todos = await _context.Todos.ToListAsync();
        return todos;
    }
}