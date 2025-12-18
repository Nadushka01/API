using TodoApi.Models;

var items = new List<TodoItem>
{
    new() { Id = 1, Title = "Купить молоко", Description = "2 литра" },
    new() { Id = 2, Title = "Подготовить отчёт", Description = "К завтрашнему дню", IsCompleted = true },
    new() { Id = 3, Title = "Позвонить маме", Description = "Вечером" },
    new() { Id = 4, Title = "Сходить в спортзал", Description = "18:00" },
    new() { Id = 5, Title = "Прочитать книгу", Description = "Глава 5", IsCompleted = true }
};

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.Run();