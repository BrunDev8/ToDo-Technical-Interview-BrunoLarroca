using TodoApi.Dtos.Item;

namespace ToDo.LogicaAplicacion.Dtos.List;

public class ListResponseDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<ItemResponseDTO>? Items { get; set; }
}