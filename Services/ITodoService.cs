using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        Task<List<TodoItemResponseDto>> GetAllTodoItemsAsync();

        Task<TodoItemResponseDto?> GetTodoItemByIdAsync(int id);

        Task<List<TodoItemResponseDto>> GetCompletedTodoItemsAsync();

        Task<List<TodoItemResponseDto>> GetPendingTodoItemsAsync();

        Task<TodoItemResponseDto> CreateTodoItemAsync(TodoItemCreateDto createDto);

        Task<TodoItemResponseDto?> UpdateTodoItemAsync(int id, TodoItemCreateDto updateDto);

        Task<bool> DeleteTodoItemAsync(int id);

        Task<bool> TodoItemExistsAsync(int id);
    }
}