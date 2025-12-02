using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using TodoApi.Dtos.Item;

namespace ToDo.LogicaAplicacion.CasosDeUso.CasosDeItem;

public interface ICrearItemCU
{
    Task<Item?> EjecutarAsync(CreateItemDTO dto);
}

public class CrearItemCU : ICrearItemCU
{
    private readonly IItemRepositorio _itemRepositorio;
    private readonly IListRepositorio _listRepositorio;

    public CrearItemCU(IItemRepositorio itemRepositorio, IListRepositorio listRepositorio)
    {
        _itemRepositorio = itemRepositorio;
        _listRepositorio = listRepositorio;
    }

    public async Task<Item?> EjecutarAsync(CreateItemDTO dto)
    {
        var listExists = await _listRepositorio.ExisteAsync(dto.ListId);
        if (!listExists)
        {
            return null;
        }

        var item = new Item
        {
            Title = dto.Name,
            Description = dto.Description,
            IsComplete = dto.IsComplete,
            ListId = dto.ListId
        };

        return await _itemRepositorio.AgregarAsync(item);
    }
}
