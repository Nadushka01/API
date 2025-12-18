using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private static readonly List<TodoItem> _items = new()
        {
            new() { Id = 1, Title = "Купить молоко", Description = "2 литра" },
            new() { Id = 2, Title = "Подготовить отчёт", Description = "К завтрашнему дню", IsCompleted = true },
            new() { Id = 3, Title = "Позвонить маме", Description = "Вечером" },
            new() { Id = 4, Title = "Сходить в спортзал", Description = "18:00" },
            new() { Id = 5, Title = "Прочитать книгу", Description = "Глава 5", IsCompleted = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return Ok(_items);
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public ActionResult<TodoItem> Post(TodoItem item)
        {
            item.Id = _items.Count > 0 ? _items.Max(i => i.Id) + 1 : 1;
            item.CreatedAt = DateTime.Now;
            _items.Add(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
    }
}