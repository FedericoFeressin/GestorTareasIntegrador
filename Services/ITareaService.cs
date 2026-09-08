using GestorTareasIntegrador.Models;

namespace GestorTareasIntegrador.Services;

/// <summary>
/// Servicio principal de gestión de tareas. Encapsula el acceso a datos
/// (EF Core + SQLite) detrás de una interfaz inyectable (Unidad 3, Clase 17).
/// </summary>
public interface ITareaService
{
    /// <summary>Obtiene todas las tareas, ordenadas por fecha de creación descendente.</summary>
    /// <returns>Lista de entidades <see cref="TareaEntity"/> con su categoría incluida.</returns>
    Task<List<TareaEntity>> ObtenerTodas();

    /// <summary>Busca una tarea por su identificador.</summary>
    /// <param name="id">Identificador de la tarea.</param>
    /// <returns>La <see cref="TareaEntity"/> encontrada o <c>null</c> si no existe.</returns>
    Task<TareaEntity?> ObtenerPorId(int id);

    /// <summary>Crea una nueva tarea en la base de datos.</summary>
    /// <param name="tarea">Entidad con los datos de la nueva tarea.</param>
    /// <returns>La entidad creada con el <c>Id</c> asignado.</returns>
    /// <exception cref="ArgumentException">Si la entidad no pasa la validación de datos.</exception>
    /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateException">Si falla la persistencia.</exception>
    Task<TareaEntity> Crear(TareaEntity tarea);

    /// <summary>Actualiza una tarea existente.</summary>
    /// <param name="tarea">Entidad con los datos modificados.</param>
    /// <returns>La entidad actualizada.</returns>
    /// <exception cref="ArgumentException">Si la entidad no pasa la validación de datos.</exception>
    /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateException">Si falla la persistencia.</exception>
    Task<TareaEntity> Actualizar(TareaEntity tarea);

    /// <summary>Elimina una tarea por su identificador (no-op si no existe).</summary>
    /// <param name="id">Identificador de la tarea a eliminar.</param>
    Task Eliminar(int id);

    /// <summary>Alterna el estado completada/pendiente de una tarea.</summary>
    /// <param name="id">Identificador de la tarea.</param>
    Task ToggleCompletar(int id);

    /// <summary>Devuelve una página de tareas aplicando filtros, orden y búsqueda.</summary>
    /// <param name="pagina">Número de página (se clampea a ≥ 1).</param>
    /// <param name="tamanioPagina">Cantidad de tareas por página (clampeado entre 1 y 100).</param>
    /// <param name="filtroEstado">Estado: <c>todas</c>, <c>pendiente</c> o <c>completada</c>.</param>
    /// <param name="filtroPrioridad">Prioridad: <c>todas</c>, <c>Alta</c>, <c>Media</c> o <c>Baja</c>.</param>
    /// <param name="busqueda">Texto para buscar por título (LIKE).</param>
    /// <returns>Resultado paginado con <see cref="PagedResult{T}"/>.</returns>
    Task<PagedResult<TareaEntity>> ObtenerPaginado(int pagina, int tamanioPagina, string? filtroEstado = null, string? filtroPrioridad = null, string? busqueda = null);

    /// <summary>Calcula estadísticas agregadas desde la base de datos.</summary>
    /// <returns>DTO con total, completadas, pendientes y conteo por prioridad.</returns>
    Task<EstadisticasDto> ObtenerEstadisticas();
}