using GestorTareasIntegrador.Models;

namespace GestorTareasIntegrador.Services;

public interface ITareaService
{
    Task<List<TareaEntity>> ObtenerTodas();
    Task<TareaEntity?> ObtenerPorId(int id);
    Task<TareaEntity> Crear(TareaEntity tarea);
    Task<TareaEntity> Actualizar(TareaEntity tarea);
    Task Eliminar(int id);
    Task ToggleCompletar(int id);
    Task<PagedResult<TareaEntity>> ObtenerPaginado(int pagina, int tamanioPagina, string? filtroEstado = null, string? filtroPrioridad = null, string? busqueda = null);
    Task<EstadisticasDto> ObtenerEstadisticas();
}
