using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoItemsController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItemResponseDto>>> GetTodoItems()
        {
            var items = await _todoService.GetAllTodoItemsAsync();
            return Ok(items);
        }

        [HttpGet("completed")]
        public async Task<ActionResult<List<TodoItemResponseDto>>> GetCompleted()
        {
            var items = await _todoService.GetCompletedTodoItemsAsync();
            return Ok(items);
        }

        [HttpGet("pending")]
        public async Task<ActionResult<List<TodoItemResponseDto>>> GetPending()
        {
            var items = await _todoService.GetPendingTodoItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemResponseDto>> GetTodoItem(int id)
        {
            var item = await _todoService.GetTodoItemByIdAsync(id);
            if (item == null)
            {
                return NotFound(new { message = $"Задача с ID {id} не найдена" });
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemResponseDto>> PostTodoItem(TodoItemCreateDto todoItemCreateDto)
        {
            var createdItem = await _todoService.CreateTodoItemAsync(todoItemCreateDto);
            return CreatedAtAction(nameof(GetTodoItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItemCreateDto todoItemCreateDto)
        {
            var updatedItem = await _todoService.UpdateTodoItemAsync(id, todoItemCreateDto);
            if (updatedItem == null)
            {
                return NotFound(new { message = $"Задача с ID {id} не найдена" });
            }

            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var result = await _todoService.DeleteTodoItemAsync(id);
            if (!result)
            {
                return NotFound(new { message = $"Задача с ID {id} не найдена" });
            }

            return NoContent();
        }
    }
}