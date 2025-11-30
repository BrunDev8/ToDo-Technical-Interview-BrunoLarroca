namespace TodoApi.Models;

public class Item
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public bool IsComplete { get; set; }
    
    // Relación con List
    public long ListId { get; set; }
    public List? List { get; set; }
}