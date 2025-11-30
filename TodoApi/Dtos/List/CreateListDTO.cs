using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos.List;

public class CreateListDTO
{
    /// <summary>
    /// Nombre de la lista de tareas
    /// </summary>
    /// <example>Tareas del hogar</example>
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 100 caracteres")]
    public required string Name { get; set; }
}