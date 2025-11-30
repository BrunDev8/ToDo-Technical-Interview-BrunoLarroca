using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Dtos.Item;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly TodoContext _context;

        public ItemController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IList<Item>>> GetItems()
        {
            return Ok(await _context.TodoItems.ToListAsync());
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/items/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutItem(long id, UpdateItemDTO payload)
        {
            var item = await _context.TodoItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.Title = payload.Title;
            item.IsComplete = payload.IsComplete;
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        // POST: api/items
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(CreateItemDTO payload)
        {
            var item = new Item 
            { 
                Title = payload.Title,
                IsComplete = payload.IsComplete
            };

            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}