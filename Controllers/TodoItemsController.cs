using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemsController(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetTodoItems()
        {
            var items = await _repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("completed")]
        public async Task<ActionResult<List<TodoItem>>> GetCompleted()
        {
            var items = await _repository.GetCompletedAsync();
            return Ok(items);
        }

        [HttpGet("pending")]
        public async Task<ActionResult<List<TodoItem>>> GetPending()
        {
            var items = await _repository.GetPendingAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            var createdItem = await _repository.CreateAsync(todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            if (!await _repository.ExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.UpdateAsync(todoItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            if (!await _repository.ExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}