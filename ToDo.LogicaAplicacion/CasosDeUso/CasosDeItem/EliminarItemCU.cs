using Dominio.InterfacesRepositorio;

namespace ToDo.LogicaAplicacion.CasosDeUso.CasosDeItem;

public interface IEliminarItemCU
{
    Task<bool> EjecutarAsync(long id);
}

public class EliminarItemCU : IEliminarItemCU
{
    private readonly IItemRepositorio _itemRepositorio;

    public EliminarItemCU(IItemRepositorio itemRepositorio)
    {
        _itemRepositorio = itemRepositorio;
    }

    public async Task<bool> EjecutarAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("El ID debe ser mayor a 0.", nameof(id));
        }

        var item = await _itemRepositorio.ObtenerPorIdAsync(id);
        
        if (item == null)
        {
            return false;
        }

        await _itemRepositorio.EliminarAsync(item);
        return true;
    }
}
