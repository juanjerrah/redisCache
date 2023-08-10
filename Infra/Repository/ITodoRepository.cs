using ToDoRedis.Models;

namespace ToDoRedis.Infra.Repository;

public interface ITodoRepository
{
    Task InserirTodos(IEnumerable<ToDo> todos);
    Task<IEnumerable<ToDo>> ObterToDos();
}