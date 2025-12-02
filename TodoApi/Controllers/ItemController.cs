using Microsoft.AspNetCore.Mvc;
using TodoApi.Dtos.Item;
using Dominio.Entidades;
using ToDo.LogicaAplicacion.CasosDeUso.CasosDeItem;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<ItemController> _logger;

        public ItemController(
            IListarItemsCU listarItemsCU,
            IObtenerItemPorIdCU obtenerItemPorIdCU,
            ICrearItemCU crearItemCU,
            IActualizarItemCU actualizarItemCU,
            IEliminarItemCU eliminarItemCU,
            ILogger<ItemController> logger)
        {
            _listarItemsCU = listarItemsCU;
            _obtenerItemPorIdCU = obtenerItemPorIdCU;
            _crearItemCU = crearItemCU;
            _actualizarItemCU = actualizarItemCU;
            _eliminarItemCU = eliminarItemCU;
            _logger = logger;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems([FromQuery] long? listId)
        {
            try
            {
                IEnumerable<Item> items;
                
                if (listId.HasValue)
                {
                    items = await _listarItemsCU.EjecutarAsync(listId.Value);
                }
                else
                {
                    items = await _listarItemsCU.EjecutarAsync();
                }
                
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de ítems");
                return StatusCode(500, new 
                { 
                    errorCode = "ITEM_LIST_ERROR",
                    message = "Error interno al obtener la lista de ítems",
                    details = ex.Message 
                });
            }
        }

        // GET: api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new 
                    { 
                        errorCode = "INVALID_ID",
                        message = "El ID debe ser mayor a 0" 
                    });
                }

                var item = await _obtenerItemPorIdCU.EjecutarAsync(id);

                if (item == null)
                {
                    return NotFound(new 
                    { 
                        errorCode = "ITEM_NOT_FOUND",
                        message = $"Item con ID {id} no encontrado" 
                    });
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el ítem con ID {ItemId}", id);
                return StatusCode(500, new 
                { 
                    errorCode = "ITEM_GET_ERROR",
                    message = "Error interno al obtener el ítem",
                    details = ex.Message 
                });
            }
        }

        // PUT: api/items/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutItem(long id, UpdateItemDTO payload)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new 
                    { 
                        errorCode = "INVALID_ID",
                        message = "El ID debe ser mayor a 0" 
                    });
                }

                var item = await _actualizarItemCU.EjecutarAsync(id, payload);

                if (item == null)
                {
                    return NotFound(new 
                    { 
                        errorCode = "ITEM_NOT_FOUND",
                        message = $"Item con ID {id} no encontrado" 
                    });
                }

                return Ok(item);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Error de validación al actualizar el ítem con ID {ItemId}", id);
                return BadRequest(new 
                { 
                    errorCode = "VALIDATION_ERROR",
                    message = "Error de validación",
                    details = ex.Message 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el ítem con ID {ItemId}", id);
                return StatusCode(500, new 
                { 
                    errorCode = "ITEM_UPDATE_ERROR",
                    message = "Error interno al actualizar el ítem",
                    details = ex.Message 
                });
            }
        }

        // POST: api/items
        [HttpPost]
        public async Task<ActionResult<ItemResponseDTO>> PostItem(CreateItemDTO payload)
        {
            try
            {
                var item = await _crearItemCU.EjecutarAsync(payload);
                
                if (item == null)
                {
                    return BadRequest(new 
                    { 
                        errorCode = "LIST_NOT_FOUND",
                        message = $"La lista con ID {payload.ListId} no existe." 
                    });
                }

                var response = new ItemResponseDTO
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    IsComplete = item.IsComplete,
                    ListId = item.ListId
                };

                return CreatedAtAction("GetItem", new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Error de validación al crear un ítem");
                return BadRequest(new 
                { 
                    errorCode = "VALIDATION_ERROR",
                    message = "Error de validación",
                    details = ex.Message 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear un ítem");
                return StatusCode(500, new 
                { 
                    errorCode = "ITEM_CREATE_ERROR",
                    message = "Error interno al crear el ítem",
                    details = ex.Message 
                });
            }
        }

        // DELETE: api/items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new 
                    { 
                        errorCode = "INVALID_ID",
                        message = "El ID debe ser mayor a 0" 
                    });
                }

                var resultado = await _eliminarItemCU.EjecutarAsync(id);
                
                if (!resultado)
                {
                    return NotFound(new 
                    { 
                        errorCode = "ITEM_NOT_FOUND",
                        message = $"Item con ID {id} no encontrado" 
                    });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el ítem con ID {ItemId}", id);
                return StatusCode(500, new 
                { 
                    errorCode = "ITEM_DELETE_ERROR",
                    message = "Error interno al eliminar el ítem",
                    details = ex.Message 
                });
            }
        }
    }
}