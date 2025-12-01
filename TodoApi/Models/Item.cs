namespace TodoApi.Models;

public class Item
{
    public long Id { get; set; }         
    public string Title { get; set; } = null!;    // Name en DB
    public string Description { get; set; } = null!;
    public bool IsComplete { get; set; } = false;
    public long ListId { get; set; }

    // Relación con List
    public List List { get; set; } = null!;
}