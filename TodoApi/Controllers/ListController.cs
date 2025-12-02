using Microsoft.AspNetCore.Mvc;
using TodoApi.Dtos.List;
using Dominio.Entidades;
using ToDo.LogicaAplicacion.CasosDeUso.CasosDeList;

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

        public ListController(
            IListarListsCU listarListsCU,
            IObtenerListPorIdCU obtenerListPorIdCU,
            ICrearListCU crearListCU,
            IActualizarListCU actualizarListCU,
            IEliminarListCU eliminarListCU)
        {
            _listarListsCU = listarListsCU;
            _obtenerListPorIdCU = obtenerListPorIdCU;
            _crearListCU = crearListCU;
            _actualizarListCU = actualizarListCU;
            _eliminarListCU = eliminarListCU;
        }

        // GET: api/lists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List>>> GetLists()
        {
            var lists = await _listarListsCU.EjecutarAsync();
            return Ok(lists);
        }

        // GET: api/lists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List>> GetList(long id)
        {
            var list = await _obtenerListPorIdCU.EjecutarAsync(id);

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
            var list = await _actualizarListCU.EjecutarAsync(id, payload);

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        // POST: api/lists
        [HttpPost]
        public async Task<ActionResult<List>> PostList(CreateListDTO payload)
        {
            var list = await _crearListCU.EjecutarAsync(payload);
            return CreatedAtAction("GetList", new { id = list.Id }, list);
        }

        // DELETE: api/lists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteList(long id)
        {
            var resultado = await _eliminarListCU.EjecutarAsync(id);
            
            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}