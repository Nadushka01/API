using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly List<TodoItem> _items = new()
        {
            new() { Id = 1, Title = "Купить молоко", Description = "2 литра" },
            new() { Id = 2, Title = "Подготовить отчёт", Description = "К завтрашнему дню", IsCompleted = true },
            new() { Id = 3, Title = "Позвонить маме", Description = "Вечером" },
            new() { Id = 4, Title = "Сходить в спортзал", Description = "18:00" },
            new() { Id = 5, Title = "Прочитать книгу", Description = "Глава 5", IsCompleted = true }
        };

        private int _nextId = 6;
        public Task<List<TodoItem>> GetAllAsync()
        {
            return Task.FromResult(_items);
        }

        public Task<TodoItem?> GetByIdAsync(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            return Task.FromResult(item);
        }

        public Task<TodoItem> CreateAsync(TodoItem entity)
        {
            entity.Id = _nextId++;
            entity.CreatedAt = DateTime.Now;
            _items.Add(entity);
            return Task.FromResult(entity);
        }

        public Task UpdateAsync(TodoItem entity)
        {
            var existing = _items.FirstOrDefault(i => i.Id == entity.Id);
            if (existing != null)
            {
                existing.Title = entity.Title;
                existing.Description = entity.Description;
                existing.IsCompleted = entity.IsCompleted;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _items.Remove(item);
            }
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(int id)
        {
            var exists = _items.Any(i => i.Id == id);
            return Task.FromResult(exists);
        }

        public Task<List<TodoItem>> GetCompletedAsync()
        {
            var completed = _items.Where(i => i.IsCompleted).ToList();
            return Task.FromResult(completed);
        }

        public Task<List<TodoItem>> GetPendingAsync()
        {
            var pending = _items.Where(i => !i.IsCompleted).ToList();
            return Task.FromResult(pending);
        }
    }
}