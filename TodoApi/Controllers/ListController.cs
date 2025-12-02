using Microsoft.AspNetCore.Mvc;
using TodoApi.Dtos.List;
using Dominio.Entidades;
using ToDo.LogicaAplicacion.CasosDeUso.CasosDeList;
using Microsoft.Extensions.Logging;
using ToDo.LogicaAplicacion.Dtos.List;

namespace TodoApi.Controllers
{
    [Route("api/lists")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListarListsCU _listarListsCU;
        private readonly IObtenerListPorIdCU _obtenerListPorIdCU;
        private readonly ICrearListCU _crearListCU;
        private readonly IActualizarListCU _actualizarListCU;
        private readonly IEliminarListCU _eliminarListCU;
        private readonly ILogger<ListController> _logger;

        public ListController(
            IListarListsCU listarListsCU,
            IObtenerListPorIdCU obtenerListPorIdCU,
            ICrearListCU crearListCU,
            IActualizarListCU actualizarListCU,
            IEliminarListCU eliminarListCU,
            ILogger<ListController> logger)
        {
            _listarListsCU = listarListsCU;
            _obtenerListPorIdCU = obtenerListPorIdCU;
            _crearListCU = crearListCU;
            _actualizarListCU = actualizarListCU;
            _eliminarListCU = eliminarListCU;
            _logger = logger;
        }

        // GET: api/lists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListResponseDTO>>> GetLists()
        {
            try
            {
                var lists = await _listarListsCU.EjecutarAsync();
                return Ok(lists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de listas");
                return StatusCode(500, new 
                { 
                    errorCode = "LIST_LIST_ERROR",
                    message = "Error interno al obtener la lista de listas",
                    details = ex.Message 
                });
            }
        }

        // GET: api/lists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List>> GetList(long id)
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

                var list = await _obtenerListPorIdCU.EjecutarAsync(id);

                if (list == null)
                {
                    return NotFound(new 
                    { 
                        errorCode = "LIST_NOT_FOUND",
                        message = $"Lista con ID {id} no encontrada" 
                    });
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista con ID {ListId}", id);
                return StatusCode(500, new 
                { 
                    errorCode = "LIST_GET_ERROR",
                    message = "Error interno al obtener la lista",
                    details = ex.Message 
                });
            }
        }

        // PUT: api/lists/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutList(long id, UpdateListDTO payload)
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

                var list = await _actualizarListCU.EjecutarAsync(id, payload);

                if (list == null)
                {
                    return NotFound(new 
                    { 
                        errorCode = "LIST_NOT_FOUND",
                        message = $"Lista con ID {id} no encontrada" 
                    });
                }

                return Ok(list);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Error de validación al actualizar la lista con ID {ListId}", id);
                return BadRequest(new 
                { 
                    errorCode = "VALIDATION_ERROR",
                    message = "Error de validación",
                    details = ex.Message 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la lista con ID {ListId}", id);
                return StatusCode(500, new 
                { 
                    errorCode = "LIST_UPDATE_ERROR",
                    message = "Error interno al actualizar la lista",
                    details = ex.Message 
                });
            }
        }

        // POST: api/lists
        [HttpPost]
        public async Task<ActionResult<List>> PostList(CreateListDTO payload)
        {
            try
            {
                var list = await _crearListCU.EjecutarAsync(payload);
                return CreatedAtAction("GetList", new { id = list.Id }, list);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Error de validación al crear una lista");
                return BadRequest(new 
                { 
                    errorCode = "VALIDATION_ERROR",
                    message = "Error de validación",
                    details = ex.Message 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear una lista");
                return StatusCode(500, new 
                { 
                    errorCode = "LIST_CREATE_ERROR",
                    message = "Error interno al crear la lista",
                    details = ex.Message 
                });
            }
        }

        // DELETE: api/lists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteList(long id)
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

                var resultado = await _eliminarListCU.EjecutarAsync(id);
                
                if (!resultado)
                {
                    return NotFound(new 
                    { 
                        errorCode = "LIST_NOT_FOUND",
                        message = $"Lista con ID {id} no encontrada" 
                    });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la lista con ID {ListId}", id);
                return StatusCode(500, new 
                { 
                    errorCode = "LIST_DELETE_ERROR",
                    message = "Error interno al eliminar la lista",
                    details = ex.Message 
                });
            }
        }
    }
}