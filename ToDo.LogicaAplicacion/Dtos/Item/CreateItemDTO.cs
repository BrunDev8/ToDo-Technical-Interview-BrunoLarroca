using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApi.Dtos.Item;

public class CreateItemDTO
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 200 caracteres")]
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [Required(ErrorMessage = "La descripcion es obligatorio")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "La descripcion debe tener entre 1 y 200 caracteres")]
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    
    [JsonPropertyName("isComplete")]
    public bool IsComplete { get; set; } = false;

    /// ID de la lista a la que pertenece el ítem
    [Required(ErrorMessage = "El ID de la lista es obligatorio")]
    [JsonPropertyName("listId")]
    public long ListId { get; set; }
}