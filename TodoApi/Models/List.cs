namespace TodoApi.Models;

public class List
{
    public long Id { get; set; }
    public required string Name { get; set; }

    //Lsta de ítems asociados
    public List<Item> Items { get; set; }
}