using TodoApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<ITodoItemRepository, TodoItemRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();