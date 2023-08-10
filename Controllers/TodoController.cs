using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToDoRedis.Infra.CachingRepository;
using ToDoRedis.Infra.Repository;
using ToDoRedis.Models;

namespace ToDoRedis.Controllers;

[Route("api/Todo")]
public class TodoController : Controller
{
    private readonly ITodoRepository _repository;
    private readonly ICachingRepository _cachingRepository;

    public TodoController(ITodoRepository repository, ICachingRepository cachingRepository)
    {
        _repository = repository;
        _cachingRepository = cachingRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDo>>> ObterToDos()
    {
        const string collectionKey = "Todos";
        var inCacheTodos = await _cachingRepository.GetCollection<ToDo>(collectionKey);
        
        if (inCacheTodos is not null)
            return Ok(inCacheTodos);
        
        var todos = await _repository.ObterToDos();
        await _cachingRepository.SetCollection(collectionKey, todos);
        return Ok(todos);
    }

    [HttpPost]
    public async Task<IActionResult> InserirTodo([FromQuery] int quantidade)
    {
        var todos = new List<ToDo>();
        for (int i = 0; i < quantidade; i++)
        {
            var todo = new ToDo
            {
                Id = Guid.NewGuid(),
                Description = $"Tarefa {i}"
            };

            todos.Add(todo);
        }

        await _repository.InserirTodos(todos);
        return Ok();
    }
}