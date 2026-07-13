using GestorTareasIntegrador.Components;
using GestorTareasIntegrador.Data;
using GestorTareasIntegrador.Services;
using GestorTareasIntegrador.State;
using Microsoft.EntityFrameworkCore;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Unidad 1 / 4: componentes Razor interactivos (Blazor Server)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Unidad 3: EF Core + SQLite con DbContextFactory (recomendado para Blazor Server)
builder.Services.AddDbContextFactory<TareasDbContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

// Unidad 1: servicio de estado reactivo (Scoped) para notificar cambios a los componentes
builder.Services.AddScoped<TareasState>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Unidad 3, Clase 22: aplicar migraciones pendientes automáticamente al iniciar.
// IMPORTANTE: esto solo APLICA migraciones que ya existan (generadas con
// `dotnet ef migrations add`). Si todavía no generaste ninguna migración,
// esto no crea nada y vas a seguir viendo "no such table" hasta que corras:
//   dotnet ef migrations add InicialConSeed
//   dotnet ef database update
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<TareasDbContext>>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    await using var ctx = await factory.CreateDbContextAsync();
    try
    {
        var pendientes = (await ctx.Database.GetPendingMigrationsAsync()).ToList();
        if (pendientes.Count > 0)
        {
            await ctx.Database.MigrateAsync();
            logger.LogInformation("Migraciones aplicadas: {Migraciones}", string.Join(", ", pendientes));
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex,
            "No se pudieron aplicar las migraciones. ¿Ya corriste 'dotnet ef migrations add InicialConSeed'?");
    }
}

app.Run();
