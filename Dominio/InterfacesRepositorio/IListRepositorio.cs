using Dominio.Entidades;

namespace Dominio.InterfacesRepositorio;

public interface IListRepositorio : IRepositorio<List>
{
    Task<List?> ObtenerConItemsAsync(long id);
    Task<IEnumerable<List>> ObtenerTodosConItemsAsync();
}
