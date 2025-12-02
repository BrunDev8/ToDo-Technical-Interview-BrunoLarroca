namespace Dominio.Entidades;

public class List
{
    public long Id { get; set; }           
    public string Name { get; set; } = null!;  

    public ICollection<Item> Items { get; set; } = new List<Item>();
}
