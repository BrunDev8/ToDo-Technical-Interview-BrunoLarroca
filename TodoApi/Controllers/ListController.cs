using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Dtos;
using TodoApi.Dtos.List;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/lists")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly TodoContext _context;

        public ListController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/lists
        [HttpGet]
        public async Task<ActionResult<IList<List>>> GetLists()
        {
            return Ok(await _context.TodoList.ToListAsync());
        }

        // GET: api/lists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List>> GetList(long id)
        {
            var list = await _context.TodoList.FindAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        // PUT: api/lists/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutList(long id, UpdateListDTO payload)
        {
            var list = await _context.TodoList.FindAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            list.Name = payload.Name;
            await _context.SaveChangesAsync();

            return Ok(list);
        }

        // POST: api/lists
        [HttpPost]
        public async Task<ActionResult<List>> PostList(CreateListDTO payload)
        {
            var list = new List { Name = payload.Name };

            _context.TodoList.Add(list);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetList", new { id = list.Id }, list);
        }

        // DELETE: api/lists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteList(long id)
        {
            var list = await _context.TodoList.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.TodoList.Remove(list);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}