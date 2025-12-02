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
        if (id <= 0)
        {
            throw new ArgumentException("El ID debe ser mayor a 0.", nameof(id));
        }

        return await _itemRepositorio.ObtenerPorIdAsync(id);
    }
}
