using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using AccesoDatos.Data;

namespace AccesoDatos.Repositorios;

public class ItemRepositorio : Repositorio<Item>, IItemRepositorio
{
    public ItemRepositorio(TodoContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Item>> ObtenerPorListaAsync(long listId)
    {
        return await _context.TodoItems
            .Where(i => i.ListId == listId)
            .ToListAsync();
    }
}
