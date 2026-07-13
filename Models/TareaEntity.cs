using System.ComponentModel.DataAnnotations;

namespace GestorTareasIntegrador.Models;

/// <summary>
/// Entidad principal del dominio. Implementa IValidatableObject para
/// validaciones cruzadas entre campos (Unidad 1, Clase 7).
/// </summary>
public class TareaEntity : IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Debe tener entre 3 y 100 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Seleccione una prioridad")]
    public string Prioridad { get; set; } = "Media";

    [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
    [DataType(DataType.Date)]
    public DateTime? FechaVencimiento { get; set; }

    public bool Completada { get; set; }

    public DateTime CreadaEn { get; set; } = DateTime.UtcNow;

    public int? CategoriaId { get; set; }
    public CategoriaEntity? Categoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Prioridad == "Alta" && FechaVencimiento.HasValue &&
            FechaVencimiento.Value.Date > DateTime.Today.AddDays(7))
        {
            yield return new ValidationResult(
                "Las tareas de prioridad Alta deben vencer dentro de los próximos 7 días.",
                new[] { nameof(FechaVencimiento) });
        }

        if (!Completada && FechaVencimiento.HasValue && FechaVencimiento.Value.Date < DateTime.Today)
        {
            yield return new ValidationResult(
                "La fecha de vencimiento no puede estar en el pasado para una tarea pendiente.",
                new[] { nameof(FechaVencimiento) });
        }
    }
}
