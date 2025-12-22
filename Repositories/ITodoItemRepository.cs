using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        Task<List<TodoItem>> GetCompletedAsync();

        Task<List<TodoItem>> GetPendingAsync();
    }
}