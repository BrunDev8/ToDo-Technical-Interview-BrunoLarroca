namespace TodoApi.Models;

public class List
{
    public long Id { get; set; }           
    public string Name { get; set; } = null!;  

    // Relación 1:N
    public ICollection<Item> Items { get; set; } = new List<Item>();
}