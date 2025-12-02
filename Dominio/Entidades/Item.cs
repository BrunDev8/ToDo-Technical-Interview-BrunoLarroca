namespace Dominio.Entidades;

public class Item
{
    public long Id { get; set; }         
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsComplete { get; set; } = false;
    public long ListId { get; set; }

    public List List { get; set; } = null!;
}
