using Microsoft.EntityFrameworkCore;
using ToDoRedis.Infra;
using ToDoRedis.Infra.CachingRepository;
using ToDoRedis.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.InstanceName = "Instance";
    opt.Configuration = "localhost:6379";
});
builder.Services.AddScoped<ITodoRepository, ToDoRepository>();
builder.Services.AddScoped<ICachingRepository, CachingRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();