using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos.Item;

public class CreateItemDTO
{
    /// <summary>
    /// Título del ítem de la tarea
    /// </summary>
    /// <example>Comprar leche</example>
    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "El título debe tener entre 1 y 200 caracteres")]
    public required string Title { get; set; }

    /// <summary>
    /// Indica si el ítem está completado
    /// </summary>
    /// <example>false</example>
    public bool IsComplete { get; set; } = false;
}