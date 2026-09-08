using GestorTareasIntegrador.Models;

namespace GestorTareasIntegrador.Services;

/// <summary>
/// Servicio de solo lectura para las <see cref="CategoriaEntity"/>.
/// </summary>
public interface ICategoriaService
{
    /// <summary>Obtiene todas las categorías.</summary>
    /// <returns>Lista de <see cref="CategoriaEntity"/>.</returns>
    Task<List<CategoriaEntity>> ObtenerTodas();
}