using Dominio.Entidades;

namespace Dominio.InterfacesRepositorio;

public interface IItemRepositorio : IRepositorio<Item>
{
    Task<IEnumerable<Item>> ObtenerPorListaAsync(long listId);
}
