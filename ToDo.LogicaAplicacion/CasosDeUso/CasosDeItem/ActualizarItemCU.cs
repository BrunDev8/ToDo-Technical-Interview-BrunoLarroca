using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using TodoApi.Dtos.Item;

namespace ToDo.LogicaAplicacion.CasosDeUso.CasosDeItem;

public interface IActualizarItemCU
{
    Task<Item?> EjecutarAsync(long id, UpdateItemDTO dto);
}

public class ActualizarItemCU : IActualizarItemCU
{
    private readonly IItemRepositorio _itemRepositorio;

    public ActualizarItemCU(IItemRepositorio itemRepositorio)
    {
        _itemRepositorio = itemRepositorio;
    }

    public async Task<Item?> EjecutarAsync(long id, UpdateItemDTO dto)
    {
        var item = await _itemRepositorio.ObtenerPorIdAsync(id);
        
        if (item == null)
        {
            return null;
        }

        item.Title = dto.Name;
        item.Description = dto.Description;
        item.IsComplete = dto.IsComplete;

        await _itemRepositorio.ActualizarAsync(item);

        return item;
    }
}
