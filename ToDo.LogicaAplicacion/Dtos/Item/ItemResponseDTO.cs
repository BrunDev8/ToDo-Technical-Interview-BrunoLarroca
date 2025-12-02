namespace TodoApi.Dtos.Item;

public class ItemResponseDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsComplete { get; set; }
    public long ListId { get; set; }
}