using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

namespace ToDo.LogicaAplicacion.CasosDeUso.CasosDeItem;

public interface IObtenerItemPorIdCU
{
    Task<Item?> EjecutarAsync(long id);
}

public class ObtenerItemPorIdCU : IObtenerItemPorIdCU
{
    private readonly IItemRepositorio _itemRepositorio;

    public ObtenerItemPorIdCU(IItemRepositorio itemRepositorio)
    {
        _itemRepositorio = itemRepositorio;
    }

    public async Task<Item?> EjecutarAsync(long id)
    {
        return await _itemRepositorio.ObtenerPorIdAsync(id);
    }
}
