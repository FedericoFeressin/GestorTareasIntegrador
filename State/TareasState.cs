using GestorTareasIntegrador.Models;
using GestorTareasIntegrador.Services;

namespace GestorTareasIntegrador.State;

/// <summary>
/// Servicio de estado reactivo (Scoped) que envuelve a ITareaService.
/// Notifica mediante OnChange a los componentes suscriptos cuando los
/// datos cambian (patrón visto en Unidad 1, Clase 8), pero persistiendo
/// contra la base de datos SQLite en vez de mantener todo en memoria.
/// </summary>
public class TareasState
{
    private readonly ITareaService _tareaService;

    public TareasState(ITareaService tareaService) => _tareaService = tareaService;

    public event Action? OnChange;

    public Task<PagedResult<TareaEntity>> CargarAsync(int pagina, int tamanio, string? estado, string? prioridad, string? busqueda)
        => _tareaService.ObtenerPaginado(pagina, tamanio, estado, prioridad, busqueda);

    public async Task CrearAsync(TareaEntity tarea)
    {
        await _tareaService.Crear(tarea);
        NotificarCambio();
    }

    public async Task ActualizarAsync(TareaEntity tarea)
    {
        await _tareaService.Actualizar(tarea);
        NotificarCambio();
    }

    public async Task EliminarAsync(int id)
    {
        await _tareaService.Eliminar(id);
        NotificarCambio();
    }

    public async Task ToggleCompletarAsync(int id)
    {
        await _tareaService.ToggleCompletar(id);
        NotificarCambio();
    }

    public Task<EstadisticasDto> ObtenerEstadisticasAsync() => _tareaService.ObtenerEstadisticas();

    private void NotificarCambio() => OnChange?.Invoke();
}
