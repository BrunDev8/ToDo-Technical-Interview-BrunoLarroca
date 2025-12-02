using Microsoft.AspNetCore.Mvc;
using TodoApi.Dtos.Item;
using Dominio.Entidades;
using ToDo.LogicaAplicacion.CasosDeUso.CasosDeItem;

namespace TodoApi.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IListarItemsCU _listarItemsCU;
        private readonly IObtenerItemPorIdCU _obtenerItemPorIdCU;
        private readonly ICrearItemCU _crearItemCU;
        private readonly IActualizarItemCU _actualizarItemCU;
        private readonly IEliminarItemCU _eliminarItemCU;

        public ItemController(
            IListarItemsCU listarItemsCU,
            IObtenerItemPorIdCU obtenerItemPorIdCU,
            ICrearItemCU crearItemCU,
            IActualizarItemCU actualizarItemCU,
            IEliminarItemCU eliminarItemCU)
        {
            _listarItemsCU = listarItemsCU;
            _obtenerItemPorIdCU = obtenerItemPorIdCU;
            _crearItemCU = crearItemCU;
            _actualizarItemCU = actualizarItemCU;
            _eliminarItemCU = eliminarItemCU;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            var items = await _listarItemsCU.EjecutarAsync();
            return Ok(items);
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(long id)
        {
            var item = await _obtenerItemPorIdCU.EjecutarAsync(id);

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
            var item = await _actualizarItemCU.EjecutarAsync(id, payload);

            if (item == null)
            {
                return NotFound(new { message = $"Item con ID {id} no encontrado" });
            }

            return Ok(item);
        }

        // POST: api/items
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(CreateItemDTO payload)
        {
            var item = await _crearItemCU.EjecutarAsync(payload);
            
            if (item == null)
            {
                return BadRequest($"La lista con ID {payload.ListId} no existe.");
            }

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(long id)
        {
            var resultado = await _eliminarItemCU.EjecutarAsync(id);
            
            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}