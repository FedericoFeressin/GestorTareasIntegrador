using Microsoft.EntityFrameworkCore;
using GestorTareasIntegrador.Data;
using GestorTareasIntegrador.Models;
using System.ComponentModel.DataAnnotations;

namespace GestorTareasIntegrador.Services;

public class TareaService : ITareaService
{
    private readonly IDbContextFactory<TareasDbContext> _factory;
    private readonly ILogger<TareaService> _logger;

    public TareaService(IDbContextFactory<TareasDbContext> factory, ILogger<TareaService> logger)
    {
        _factory = factory;
        _logger = logger;
    }

    public async Task<List<TareaEntity>> ObtenerTodas()
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        return await ctx.Tareas.Include(t => t.Categoria)
            .OrderByDescending(t => t.CreadaEn)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TareaEntity?> ObtenerPorId(int id)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        return await ctx.Tareas.Include(t => t.Categoria)
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TareaEntity> Crear(TareaEntity tarea)
    {
        try
        {
            Validar(tarea);
            await using var ctx = await _factory.CreateDbContextAsync();
            tarea.CreadaEn = DateTime.UtcNow;
            ctx.Tareas.Add(tarea);
            await ctx.SaveChangesAsync();
            _logger.LogInformation("Tarea creada: {Titulo}", tarea.Titulo);
            return tarea;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error al crear la tarea {Titulo}", tarea.Titulo);
            throw;
        }
    }

    public async Task<TareaEntity> Actualizar(TareaEntity tarea)
    {
        try
        {
            Validar(tarea);
            await using var ctx = await _factory.CreateDbContextAsync();
            ctx.Tareas.Update(tarea);
            await ctx.SaveChangesAsync();
            _logger.LogInformation("Tarea actualizada: {Id}", tarea.Id);
            return tarea;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error al actualizar la tarea {Id}", tarea.Id);
            throw;
        }
    }

    public async Task Eliminar(int id)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        var t = await ctx.Tareas.FindAsync(id);
        if (t is not null)
        {
            ctx.Tareas.Remove(t);
            await ctx.SaveChangesAsync();
            _logger.LogInformation("Tarea eliminada: {Id}", id);
        }
    }

    public async Task ToggleCompletar(int id)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        var t = await ctx.Tareas.FindAsync(id);
        if (t is not null)
        {
            t.Completada = !t.Completada;
            await ctx.SaveChangesAsync();
        }
    }

    private static void Validar(TareaEntity tarea)
    {
        var context = new ValidationContext(tarea);
        var resultados = new List<ValidationResult>();
        if (!Validator.TryValidateObject(tarea, context, resultados, validateAllProperties: true))
        {
            var mensaje = string.Join(" ", resultados.Select(r => r.ErrorMessage));
            throw new ArgumentException($"La tarea no es válida: {mensaje}");
        }
    }

    public async Task<PagedResult<TareaEntity>> ObtenerPaginado(int pagina, int tamanioPagina, string? filtroEstado = null, string? filtroPrioridad = null, string? busqueda = null)
    {
        pagina = Math.Max(1, pagina);
        tamanioPagina = Math.Clamp(tamanioPagina, 1, 100);

        await using var ctx = await _factory.CreateDbContextAsync();
        var query = ctx.Tareas.Include(t => t.Categoria).AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtroEstado) && filtroEstado != "todas")
        {
            query = filtroEstado == "completada"
                ? query.Where(t => t.Completada)
                : query.Where(t => !t.Completada);
        }

        if (!string.IsNullOrWhiteSpace(filtroPrioridad) && filtroPrioridad != "todas")
        {
            query = query.Where(t => t.Prioridad == filtroPrioridad);
        }

        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            query = query.Where(t => t.Titulo.Contains(busqueda));
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(t => t.Completada)
            .ThenBy(t => t.FechaVencimiento)
            .Skip((pagina - 1) * tamanioPagina)
            .Take(tamanioPagina)
            .ToListAsync();

        return new PagedResult<TareaEntity>
        {
            Items = items,
            PaginaActual = pagina,
            TotalRegistros = total,
            TotalPaginas = Math.Max(1, (int)Math.Ceiling(total / (double)tamanioPagina))
        };
    }

    public async Task<EstadisticasDto> ObtenerEstadisticas()
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        var tareas = await ctx.Tareas.AsNoTracking().ToListAsync();

        return new EstadisticasDto
        {
            Total = tareas.Count,
            Completadas = tareas.Count(t => t.Completada),
            Pendientes = tareas.Count(t => !t.Completada),
            Altas = tareas.Count(t => t.Prioridad == "Alta"),
            Medias = tareas.Count(t => t.Prioridad == "Media"),
            Bajas = tareas.Count(t => t.Prioridad == "Baja")
        };
    }
}
