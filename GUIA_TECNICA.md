# Guía técnica — Gestor de Tareas Integrador

> Para evaluadores: cada tabla incluye la ubicación exacta del código con número de línea.
> En VS Code, Ctrl+Click sobre las rutas para navegar directamente.

---

## 1. Arquitectura general

```
┌─────────────────────────────────────────────────────────┐
│  Blazor Server (.NET 10, InteractiveServer render)     │
├──────────────┬──────────────┬───────────────────────────┤
│  Pages/      │  Shared/     │  Layout/                  │
│  (rutas)     │  (reusables) │  (MainLayout, NavMenu)    │
├──────────────┴──────────────┴───────────────────────────┤
│  TareasState (servicio reactivo Scoped)                 │
├─────────────────────────────────────────────────────────┤
│  ITareaService / ICategoriaService (interfaces)         │
├─────────────────────────────────────────────────────────┤
│  TareaService / CategoriaService (implementación)       │
├─────────────────────────────────────────────────────────┤
│  EF Core + SQLite (IDbContextFactory)                   │
└─────────────────────────────────────────────────────────┘
```

---

## 2. Tabla de componentes y ubicación del código

### Unidad 1 — Componentes Razor y routing

| Archivo | Línea(s) clave | Concepto de cátedra |
|---------|-----------------|---------------------|
| `Models/TareaEntity.cs` | :9 `IValidatableObject`, :34 `Validate()` | Validación cruzada entre campos (Clase 7) |
| `Components/Shared/TareaItem.razor` | :41 `[Parameter]`, :42-43 `EventCallback` | Parámetros y comunicación padre-hijo (Clase 6) |
| `Components/Shared/Paginacion.razor` | :25-27 `[Parameter]`, :29 `OnCambio.InvokeAsync` | EventCallback para paginación (Clase 6) |
| `Components/Shared/FiltroTareas.razor` | :24-28 `[Parameter]` + `Changed` | Two-way binding manual con EventCallback (Clase 6) |
| `Components/Shared/ConfirmDialog.razor` | :18 `Visible`, :21-22 `EventCallback` | Componente condicional con parámetros (Clase 6) |
| `Components/Shared/AlertError.razor` | :10 `Mensaje`, :11 `OnCerrar` | Componente reutilizable de alerta (Clase 6) |
| `Components/Shared/EstadisticasBar.razor` | :40 `[Parameter] Estadisticas` | Presentación de datos con parámetros (Clase 6) |
| `Components/Shared/DarkModeToggle.razor` | :12 `OnAfterRenderAsync` | Lifecycle post-render (Clase 5) |
| `Components/Pages/Tareas.razor` | :1 `@page "/"`, :3 `@page "/tareas/{FiltroEstado}"` | Routing con parámetros (Clase 4) |
| `Components/Pages/Tareas.razor` | :51 `[Parameter] public string? FiltroEstado` | Binding de parámetros de ruta (Clase 4) |
| `Components/Pages/Tareas.razor` | :66 `OnInitializedAsync`, :151 `IDisposable` | Lifecycle + limpieza de suscripciones (Clase 5) |
| `Components/Pages/Tareas.razor` | :95-99 `AlCambiarEstado` + `InvokeAsync` | Patrón observer con StateHasChanged (Clase 8) |
| `Components/Pages/NuevaTarea.razor` | :8-10 `EditForm` + `DataAnnotationsValidator` | Formularios con validación (Clase 7) |
| `Components/Pages/EditarTarea.razor` | :1 `@page "/editar-tarea/{Id:int}"` | Routing con parámetro entero (Clase 4) |
| `Components/Pages/TareaDetalle.razor` | :2 `IAsyncDisposable`, :50 `IJSObjectReference` | JS Interop + disposal (Clase 13) |
| `State/TareasState.cs` | :18 `event Action? OnChange`, :49 `NotificarCambio()` | Servicio reactivo Scoped (Clase 8) |

### Unidad 2 — HTML, CSS, Bootstrap y JS Interop

| Archivo | Línea(s) clave | Concepto de cátedra |
|---------|-----------------|---------------------|
| `Components/App.razor` | :7-8 Bootstrap CDN, :14 `blazor.web.js` | CDN de Bootstrap + framework JS (Clase 11) |
| `Components/App.razor` | :15 Bootstrap JS bundle | JS Interop - menú hamburguesa (Clase 13) |
| `Components/Layout/MainLayout.razor` | :1 `LayoutComponentBase`, :4 `header`, :8 `main`, :14 `footer` | HTML5 semántico + layout (Clase 9) |
| `Components/Layout/NavMenu.razor` | :1 `navbar-dark bg-dark`, :5 `data-bs-toggle="collapse"` | Bootstrap navbar responsive (Clase 12) |
| `wwwroot/css/app.css` | :1-6 `:root` variables CSS, :15 `body.dark-mode` | Variables CSS + modo oscuro (Clase 10) |
| `wwwroot/css/app.css` | :38-131 Reglas dark-mode completas | Estilos globales para modo oscuro |
| `Components/Shared/TareaItem.razor.css` | :1-26 | CSS Isolation (Clase 10) |
| `Components/Shared/ConfirmDialog.razor.css` | :1-30 | CSS Isolation - modal puro HTML+CSS (Clase 10) |
| `Components/Shared/EstadisticasBar.razor.css` | :1-19 | CSS Isolation - estadísticas (Clase 10) |
| `wwwroot/js/interop.js` | :4-16 Funciones `aplicarModoOscuro`, `copiarAlPortapapeles` | JS Interop módulo ES6 (Clase 13) |
| `Components/Shared/DarkModeToggle.razor` | :16 `InvokeAsync<IJSObjectReference>("import", ...)` | Carga dinámica de módulo JS (Clase 13) |
| `Components/Pages/TareaDetalle.razor` | :50-52 `InvokeVoidAsync("copiarAlPortapapeles")` | Invocación de función JS (Clase 13) |

### Unidad 3 — Entity Framework Core

| Archivo | Línea(s) clave | Concepto de cátedra |
|---------|-----------------|---------------------|
| `Data/TareasDbContext.cs` | :6 `DbContext`, :10-11 `DbSet<T>` | DbContext y DbSet (Clase 15) |
| `Data/TareasDbContext.cs` | :15-21 Fluent API `HasMany/WithOne/ForeignKey/OnDelete` | Relaciones uno-a-muchos (Clase 15) |
| `Data/TareasDbContext.cs` | :23-27 `HasIndex` | Índices en columnas (Clase 15) |
| `Data/TareasDbContext.cs` | :30-50 `HasData()` | Datos de seed (Clase 16) |
| `Models/CategoriaEntity.cs` | :5-13 | Entidad secundaria con Data Annotations (Clase 15) |
| `Models/TareaEntity.cs` | :9-50 | Entidad principal con validaciones (Clase 15) |
| `Program.cs` | :15-16 `AddDbContextFactory` + `UseSqlite` | Configuración de EF Core + SQLite (Clase 14) |
| `Program.cs` | :45-64 Auto-migración al iniciar | Migraciones automáticas (Clase 16) |
| `Services/ITareaService.cs` | :5-15 | Interfaz del servicio (Clase 17) |
| `Services/TareaService.cs` | :9 `_factory`, :20 `CreateDbContextAsync` | Patrón DbContextFactory (Clase 14) |
| `Services/TareaService.cs` | :93-131 `ObtenerPaginado` | Consulta con Skip/Take + Count (Clase 17) |
| `Services/TareaService.cs` | :110-113 `Contains(busqueda)` | Búsqueda LIKE (Clase 17) |
| `Services/CategoriaService.cs` | :7-17 | Servicio de solo lectura (Clase 17) |
| `Migrations/` | (carpeta) | Migraciones generadas con dotnet-ef (Clase 16) |

### Unidad 4 — Git, CI/CD y documentación

| Archivo | Línea(s) clave | Concepto de cátedra |
|---------|-----------------|---------------------|
| `.github/workflows/dotnet.yml` | :1-23 | CI/CD con GitHub Actions (Clase 22) |
| `.gitignore` | (raíz) | Ignorar archivos de build/bin (Clase 19) |
| `README.md` | (raíz) | Documentación profesional (Clase 21) |
| `CHANGELOG.md` | (raíz) | Historial de versiones (Clase 21) |
| `appsettings.json` | :2-4 ConnectionStrings | Configuración por entorno (Clase 22) |
| `appsettings.Development.json` | :2-4 | Configuración separada para dev (Clase 22) |

---

## 3. Patrones de diseño utilizados

| Patrón | Dónde | Archivo |
|--------|-------|---------|
| **Observer** | `TareasState.OnChange` se dispara y los componentes suscriptos reaccionan | `State/TareasState.cs:18`, `Components/Pages/Tareas.razor:66-68` |
| **Service Layer** | Acceso a datos abstraído detrás de interfaces `ITareaService`/`ICategoriaService` | `Services/ITareaService.cs`, `Services/ICategoriaService.cs` |
| **DbContextFactory** | Cada operación crea su propio `DbContext` (seguro para Blazor Server) | `Services/TareaService.cs:20,29,39,...` |
| **Component Composition** | Páginas compuestas por componentes reutilizables con `@rendermode InteractiveServer` | `Components/Pages/Tareas.razor:9-41` |

---

## 4. Base de datos

- **Motor:** SQLite (`gestor_tareas.db` en modo Production, `gestor_tareas_dev.db` en Development)
- **Entidades:** `TareaEntity` (10 registros seed) y `CategoriaEntity` (5 registros seed)
- **Relación:** Una categoría tiene muchas tareas (`OnDelete: SetNull`)
- **Migraciones:** Se aplican automáticamente al iniciar la app si existen migraciones pendientes
  (`Program.cs:45-64`)

---

## 5. JS Interop

- **Módulo ES6:** `wwwroot/js/interop.js` cargado bajo demanda con `import()`
- **Funciones:**
  - `aplicarModoOscuro(activo)` — alterna clase `dark-mode` en `<body>`
  - `guardarPreferenciaOscura(activo)` — persiste en `localStorage`
  - `obtenerPreferenciaOscura()` — lee de `localStorage`
  - `copiarAlPortapapeles(texto)` — usa `navigator.clipboard.writeText()`
- **Disposal:** Ambos componentes (`DarkModeToggle`, `TareaDetalle`) implementan
  `IAsyncDisposable` y llaman `_modulo.DisposeAsync()` con `try/catch` para
  `JSDisconnectedException`

---

## 6. Validaciones

- **Data Annotations en `TareaEntity`:** `[Required]`, `[StringLength]`, `[DataType(DataType.Date)]`
- **Validación cruzada (`IValidatableObject`):** Tareas de prioridad Alta deben vencer dentro
  de 7 días; tareas pendientes no pueden tener fecha pasada
- **En la UI:** `EditForm` + `DataAnnotationsValidator` + `ValidationMessage` en cada campo
