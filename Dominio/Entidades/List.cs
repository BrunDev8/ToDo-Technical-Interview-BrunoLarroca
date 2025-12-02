using Dominio.Interfaces;

namespace Dominio.Entidades;

public class List : IValidate
{
    public long Id { get; set; }           
    public string Name { get; set; } = null!;  

    public ICollection<Item> Items { get; set; } = new List<Item>();

    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new ArgumentException("El nombre de la lista no puede estar vacío.", nameof(Name));
        }

        if (Name.Length > 200)
        {
            throw new ArgumentException("El nombre de la lista no puede exceder los 200 caracteres.", nameof(Name));
        }
    }
}
