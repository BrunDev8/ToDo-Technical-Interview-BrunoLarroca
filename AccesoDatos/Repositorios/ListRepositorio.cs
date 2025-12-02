using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using AccesoDatos.Data;

namespace AccesoDatos.Repositorios;

public class ListRepositorio : Repositorio<List>, IListRepositorio
{
    public ListRepositorio(TodoContext context) : base(context)
    {
    }

    public async Task<List?> ObtenerConItemsAsync(long id)
    {
        return await _context.TodoList
            .Include(l => l.Items)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<IEnumerable<List>> ObtenerTodosConItemsAsync()
    {
        return await _context.TodoList
            .Include(l => l.Items)
            .ToListAsync();
    }
}
