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
        var item = await _itemRepositorio.ObtenerPorIdAsync(id);
        
        if (item == null)
        {
            return false;
        }

        await _itemRepositorio.EliminarAsync(item);
        return true;
    }
}
