using Microsoft.EntityFrameworkCore;
using GestorTareasIntegrador.Models;

namespace GestorTareasIntegrador.Data;

public class TareasDbContext : DbContext
{
    public TareasDbContext(DbContextOptions<TareasDbContext> options) : base(options) { }

    public DbSet<TareaEntity> Tareas => Set<TareaEntity>();
    public DbSet<CategoriaEntity> Categorias => Set<CategoriaEntity>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<CategoriaEntity>(e =>
        {
            e.HasMany(c => c.Tareas)
             .WithOne(t => t.Categoria)
             .HasForeignKey(t => t.CategoriaId)
             .OnDelete(DeleteBehavior.SetNull);
        });

        mb.Entity<TareaEntity>(e =>
        {
            e.HasIndex(t => t.Prioridad);
            e.HasIndex(t => t.Titulo);
        });

        // --- Seed data (Unidad 3, Clase 16) ---
        mb.Entity<CategoriaEntity>().HasData(
            new CategoriaEntity { Id = 1, Nombre = "Estudio" },
            new CategoriaEntity { Id = 2, Nombre = "Trabajo" },
            new CategoriaEntity { Id = 3, Nombre = "Personal" },
            new CategoriaEntity { Id = 4, Nombre = "Hogar" },
            new CategoriaEntity { Id = 5, Nombre = "Proyectos" }
        );

        var hoy = new DateTime(2026, 7, 1);
        mb.Entity<TareaEntity>().HasData(
            new TareaEntity { Id = 1, Titulo = "Instalar .NET 10 SDK", Descripcion = "Verificar con dotnet --version", Prioridad = "Alta", FechaVencimiento = hoy.AddDays(1), Completada = true, CreadaEn = hoy.AddDays(-10), CategoriaId = 1 },
            new TareaEntity { Id = 2, Titulo = "Repasar Data Binding", Descripcion = "Clase 3 - binding bidireccional", Prioridad = "Media", FechaVencimiento = hoy.AddDays(5), Completada = false, CreadaEn = hoy.AddDays(-9), CategoriaId = 1 },
            new TareaEntity { Id = 3, Titulo = "Armar EditForm de alta", Descripcion = "Con DataAnnotationsValidator", Prioridad = "Alta", FechaVencimiento = hoy.AddDays(2), Completada = false, CreadaEn = hoy.AddDays(-8), CategoriaId = 5 },
            new TareaEntity { Id = 4, Titulo = "Configurar CSS Isolation", Descripcion = "Archivos .razor.css", Prioridad = "Baja", FechaVencimiento = hoy.AddDays(12), Completada = false, CreadaEn = hoy.AddDays(-7), CategoriaId = 5 },
            new TareaEntity { Id = 5, Titulo = "Aplicar grilla Bootstrap", Descripcion = "Cards responsive", Prioridad = "Media", FechaVencimiento = hoy.AddDays(8), Completada = false, CreadaEn = hoy.AddDays(-6), CategoriaId = 5 },
            new TareaEntity { Id = 6, Titulo = "Implementar modo oscuro", Descripcion = "JS Interop + localStorage", Prioridad = "Media", FechaVencimiento = hoy.AddDays(9), Completada = false, CreadaEn = hoy.AddDays(-5), CategoriaId = 5 },
            new TareaEntity { Id = 7, Titulo = "Crear TareasDbContext", Descripcion = "EF Core + SQLite", Prioridad = "Alta", FechaVencimiento = hoy.AddDays(3), Completada = true, CreadaEn = hoy.AddDays(-4), CategoriaId = 2 },
            new TareaEntity { Id = 8, Titulo = "Migraciones y seed", Descripcion = "dotnet ef migrations add", Prioridad = "Alta", FechaVencimiento = hoy.AddDays(4), Completada = false, CreadaEn = hoy.AddDays(-3), CategoriaId = 2 },
            new TareaEntity { Id = 9, Titulo = "Agregar paginación", Descripcion = "Skip/Take con conteo total", Prioridad = "Media", FechaVencimiento = hoy.AddDays(10), Completada = false, CreadaEn = hoy.AddDays(-2), CategoriaId = 3 },
            new TareaEntity { Id = 10, Titulo = "Preparar demo final", Descripcion = "Revisar checklist de entrega", Prioridad = "Baja", FechaVencimiento = hoy.AddDays(20), Completada = false, CreadaEn = hoy.AddDays(-1), CategoriaId = 4 }
        );
    }
}
