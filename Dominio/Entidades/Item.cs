using Dominio.Interfaces;

namespace Dominio.Entidades;

public class Item : IValidate
{
    public long Id { get; set; }         
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsComplete { get; set; } = false;
    public long ListId { get; set; }

    public List List { get; set; } = null!;

    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            throw new ArgumentException("El título del ítem no puede estar vacío.", nameof(Title));
        }

        if (Title.Length > 200)
        {
            throw new ArgumentException("El título del ítem no puede exceder los 200 caracteres.", nameof(Title));
        }

        if (string.IsNullOrWhiteSpace(Description))
        {
            throw new ArgumentException("La descripción del ítem no puede estar vacía.", nameof(Description));
        }

        if (Description.Length > 1000)
        {
            throw new ArgumentException("La descripción del ítem no puede exceder los 1000 caracteres.", nameof(Description));
        }

        if (ListId <= 0)
        {
            throw new ArgumentException("El ítem debe estar asociado a una lista válida.", nameof(ListId));
        }
    }
}
