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
            var item = await _context.TodoItems
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound(new { message = $"Item con ID {id} no encontrado" });
            }

            item.Title = payload.Title;
            item.Description = payload.Description;
            item.IsComplete = payload.IsComplete;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.TodoItems.AnyAsync(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return Ok(item);
        }

        // POST: api/items
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(CreateItemDTO payload)
        {
            // Verificar que la lista existe
            var listExists = await _context.TodoList.AnyAsync(l => l.Id == payload.ListId);
            if (!listExists)
            {
                return BadRequest($"La lista con ID {payload.ListId} no existe.");
            }

            var item = new Item 
            { 
                Title = payload.Title,
                Description = payload.Description,
                IsComplete = payload.IsComplete,
                ListId = payload.ListId
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