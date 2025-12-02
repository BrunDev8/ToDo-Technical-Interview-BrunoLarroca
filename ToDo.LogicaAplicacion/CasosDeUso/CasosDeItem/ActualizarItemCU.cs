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
        if (id <= 0)
        {
            throw new ArgumentException("El ID debe ser mayor a 0.", nameof(id));
        }

        var item = await _itemRepositorio.ObtenerPorIdAsync(id);
        
        if (item == null)
        {
            return null;
        }

        // Solo actualizar los campos que se enviaron
        if (!string.IsNullOrWhiteSpace(dto.Name))
        {
            item.Title = dto.Name;
        }

        if (!string.IsNullOrWhiteSpace(dto.Description))
        {
            item.Description = dto.Description;
        }

        item.IsComplete = dto.IsComplete;

        // Validar antes de guardar
        item.Validar();

        await _itemRepositorio.ActualizarAsync(item);

        return item;
    }
}
