using Microsoft.EntityFrameworkCore;
using GestorTareasIntegrador.Data;
using GestorTareasIntegrador.Models;

namespace GestorTareasIntegrador.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IDbContextFactory<TareasDbContext> _factory;

    public CategoriaService(IDbContextFactory<TareasDbContext> factory) => _factory = factory;

    public async Task<List<CategoriaEntity>> ObtenerTodas()
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        return await ctx.Categorias.AsNoTracking().OrderBy(c => c.Nombre).ToListAsync();
    }
}
