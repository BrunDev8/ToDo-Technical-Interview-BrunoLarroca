using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

namespace ToDo.LogicaAplicacion.CasosDeUso.CasosDeItem;

public interface IListarItemsCU
{
    Task<IEnumerable<Item>> EjecutarAsync();
}

public class ListarItemsCU : IListarItemsCU
{
    private readonly IItemRepositorio _itemRepositorio;

    public ListarItemsCU(IItemRepositorio itemRepositorio)
    {
        _itemRepositorio = itemRepositorio;
    }

    public async Task<IEnumerable<Item>> EjecutarAsync()
    {
        return await _itemRepositorio.ObtenerTodosAsync();
    }
}
