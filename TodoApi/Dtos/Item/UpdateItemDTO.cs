using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos.Item;

public class UpdateItemDTO
{
    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "El título debe tener entre 1 y 200 caracteres")]
    public required string Title { get; set; }

     [Required(ErrorMessage = "La descripcion es obligatorio")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "La descripcion debe tener entre 1 y 200 caracteres")]
    public required string Description { get; set; }


    public bool IsComplete { get; set; }
}