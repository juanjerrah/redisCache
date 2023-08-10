using Microsoft.EntityFrameworkCore;
using ToDoRedis.Models;

namespace ToDoRedis.Infra;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {}

    public DbSet<ToDo> Todos { get; set; }
}