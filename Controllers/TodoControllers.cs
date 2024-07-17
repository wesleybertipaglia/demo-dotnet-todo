namespace todo.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo.Data;
using todo.Models;

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
    public async Task<ActionResult<IEnumerable<Todo>>> List()
    {
        return await _context.Todos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> Get(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        return todo;
    }
}