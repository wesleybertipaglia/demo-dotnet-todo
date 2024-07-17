namespace todo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using todo.Data;
    using todo.Models;
    using todo.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDBContext _context;

        public UserController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> List()
        {
            var users = await _context.Users.ToListAsync();
            var userDtos = users.Select(user => new UserResponseDTO(user.Id, user.Name, user.Email)).ToList();
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> Get(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserResponseDTO(user.Id, user.Name, user.Email);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> Create(UserRequestDTO userRequest)
        {
            var user = new User(userRequest.name, userRequest.email, userRequest.password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = new UserResponseDTO(user.Id, user.Name, user.Email);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, userDto);
        }
    }
}
