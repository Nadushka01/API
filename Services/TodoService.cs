// TodoApi/Services/TodoService.cs
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoItemRepository _repository;

        public TodoService(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TodoItemResponseDto>> GetAllTodoItemsAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(MapToDto).ToList();
        }

        public async Task<TodoItemResponseDto?> GetTodoItemByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<List<TodoItemResponseDto>> GetCompletedTodoItemsAsync()
        {
            var items = await _repository.GetCompletedAsync();
            return items.Select(MapToDto).ToList();
        }

        public async Task<List<TodoItemResponseDto>> GetPendingTodoItemsAsync()
        {
            var items = await _repository.GetPendingAsync();
            return items.Select(MapToDto).ToList();
        }

        public async Task<TodoItemResponseDto> CreateTodoItemAsync(TodoItemCreateDto createDto)
        {
            var todoItem = new TodoItem
            {
                Title = createDto.Title,
                Description = createDto.Description,
                IsCompleted = createDto.IsCompleted,
                CreatedAt = DateTime.Now
            };

            var createdItem = await _repository.CreateAsync(todoItem);
            return MapToDto(createdItem);
        }

        public async Task<TodoItemResponseDto?> UpdateTodoItemAsync(int id, TodoItemCreateDto updateDto)
        {
            if (!await _repository.ExistsAsync(id))
                return null;

            var existingItem = await _repository.GetByIdAsync(id);
            if (existingItem == null)
                return null;

            existingItem.Title = updateDto.Title;
            existingItem.Description = updateDto.Description;
            existingItem.IsCompleted = updateDto.IsCompleted;

            await _repository.UpdateAsync(existingItem);

            return MapToDto(existingItem);
        }

        public async Task<bool> DeleteTodoItemAsync(int id)
        {
            if (!await _repository.ExistsAsync(id))
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> TodoItemExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }

        private TodoItemResponseDto MapToDto(TodoItem item)
        {
            return new TodoItemResponseDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                CreatedAt = item.CreatedAt
            };
        }
    }
}