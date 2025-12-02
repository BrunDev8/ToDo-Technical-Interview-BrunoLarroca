using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos.Item;

public class UpdateItemDTO
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 200 caracteres")]
    public required string Name { get; set; }

     [Required(ErrorMessage = "La descripcion es obligatorio")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "La descripcion debe tener entre 1 y 200 caracteres")]
    public required string Description { get; set; }


    public bool IsComplete { get; set; }
}