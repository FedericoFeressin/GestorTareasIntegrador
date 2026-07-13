using System.ComponentModel.DataAnnotations;

namespace GestorTareasIntegrador.Models;

public class CategoriaEntity
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Debe tener entre 2 y 50 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    public List<TareaEntity> Tareas { get; set; } = new();
}
