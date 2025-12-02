using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using ToDo.LogicaAplicacion.Dtos.List;
using TodoApi.Dtos.Item;

namespace ToDo.LogicaAplicacion.CasosDeUso.CasosDeList;

public interface IListarListsCU
{
    Task<IEnumerable<ListResponseDTO>> EjecutarAsync();
}

public class ListarListsCU : IListarListsCU
{
    private readonly IListRepositorio _listRepositorio;

    public ListarListsCU(IListRepositorio listRepositorio)
    {
        _listRepositorio = listRepositorio;
    }

    public async Task<IEnumerable<ListResponseDTO>> EjecutarAsync()
    {
        var lists = await _listRepositorio.ObtenerTodosConItemsAsync();
        
        return lists.Select(list => new ListResponseDTO
        {
            Id = list.Id,
            Name = list.Name,
            Items = list.Items?.Select(item => new ItemResponseDTO
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsComplete = item.IsComplete,
                ListId = item.ListId
            })
        });
    }
}
