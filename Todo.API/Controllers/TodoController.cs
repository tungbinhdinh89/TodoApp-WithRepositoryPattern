using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Todo.Application.Services;
using ToDoList.Core.Entities;
using ToDoList.Shared.DTOs;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController(ToDoServices _toDoServices) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? title)
        {
            Expression<Func<ToDoItem, bool>>? filter = null;

            if (!string.IsNullOrEmpty(title))
            {
                filter = item => item.Title.Contains(title);
            }

            var items = await _toDoServices.GetAllAsync(filter);
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _toDoServices.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound(new { Message = $"Todo with ID {id} not found." });
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ToDoItemDTO toDoItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _toDoServices.AddAsync(toDoItemDto);
            return CreatedAtAction(nameof(GetById), new { id = toDoItemDto.Id }, toDoItemDto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ToDoItemDTO toDoItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTodo = await _toDoServices.GetByIdAsync(id);
            if (existingTodo == null)
            {
                return NotFound(new { Message = $"Todo with ID {id} not found." });
            }

            toDoItemDto.Id = id;
            await _toDoServices.UpdateAsync(toDoItemDto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTodo = await _toDoServices.GetByIdAsync(id);
            if (existingTodo == null)
            {
                return NotFound(new { Message = $"Todo with ID {id} not found." });
            }

            await _toDoServices.DeleteAsync(id);
            return NoContent();
        }
    }
}
