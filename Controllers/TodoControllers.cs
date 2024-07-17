namespace todo.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo.Data;
using todo.Models;
using todo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly AppDBContext _context;

    public TodoController(AppDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoResponseDTO>>> List()
    {
        var todos = await _context.Todos.Include(t => t.User).ToListAsync();
        var todoDtos = todos.Select(todo => new TodoResponseDTO(
            todo.Id, todo.Title, todo.Description, todo.Status, todo.User.Id)).ToList();
        return Ok(todoDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoResponseDTO>> Get(Guid id)
    {
        var todo = await _context.Todos.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

        if (todo == null)
        {
            return NotFound();
        }

        var todoDto = new TodoResponseDTO(todo.Id, todo.Title, todo.Description, todo.Status, todo.User.Id);
        return Ok(todoDto);
    }

    [HttpPost]
    public async Task<ActionResult<TodoResponseDTO>> Create(TodoRequestDTO todoRequest)
    {
        var user = await _context.Users.FindAsync(todoRequest.userId);
        if (user == null)
        {
            return BadRequest("Invalid UserId");
        }

        var todo = new Todo
        {
            Title = todoRequest.title,
            Description = todoRequest.description,
            Status = todoRequest.status,
            User = user
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        var todoDto = new TodoResponseDTO(todo.Id, todo.Title, todo.Description, todo.Status, todo.User.Id);
        return CreatedAtAction(nameof(Get), new { id = todo.Id }, todoDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, TodoRequestDTO todoRequest)
    {
        var todo = await _context.Todos.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);

        if (todo == null)
        {
            return NotFound();
        }

        var user = await _context.Users.FindAsync(todoRequest.userId);
        if (user == null)
        {
            return BadRequest("Invalid UserId");
        }

        todo.Title = todoRequest.title;
        todo.Description = todoRequest.description;
        todo.Status = todoRequest.status;
        todo.User = user;

        _context.Entry(todo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null)
        {
            return NotFound();
        }

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoExists(Guid id)
    {
        return _context.Todos.Any(e => e.Id == id);
    }
}
