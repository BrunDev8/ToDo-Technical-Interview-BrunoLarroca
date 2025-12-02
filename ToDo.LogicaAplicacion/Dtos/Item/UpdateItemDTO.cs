using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos.Item;

public class UpdateItemDTO
{
    [StringLength(200, MinimumLength = 1, ErrorMessage = "El nombre debe tener entre 1 y 200 caracteres")]
    public string? Name { get; set; }

    [StringLength(1000, MinimumLength = 1, ErrorMessage = "La descripcion debe tener entre 1 y 1000 caracteres")]
    public string? Description { get; set; }

    public bool IsComplete { get; set; }
}