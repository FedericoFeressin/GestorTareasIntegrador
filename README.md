# Gestor de Tareas — Proyecto Integrador Blazor

Aplicación Blazor Server (CRUD completo) que integra los contenidos de las 4 unidades de la
materia: componentes Razor, interfaz visual con Bootstrap y JS Interop, persistencia con EF Core
+ SQLite, y prácticas profesionales (Git, CI/CD).

## Stack tecnológico

- **Frontend:** Blazor Server, plantilla "Blazor Web App" (render mode `InteractiveServer`)
- **Backend/Datos:** Entity Framework Core 10, SQLite, patrón `IDbContextFactory`
- **Estilos:** Bootstrap 5.3, Bootstrap Icons, CSS Isolation, variables CSS, modo oscuro
- **JS Interop:** módulo ES6 (`wwwroot/js/interop.js`) para modo oscuro persistente y portapapeles
- **CI:** GitHub Actions (`.github/workflows/dotnet.yml`)

## ⚠️ Nota importante sobre versiones (.NET 10 vs .NET 8)

El material de cátedra usa **.NET 8**, pero vos tenés instalado **.NET 10**. El proyecto está
configurado con `<TargetFramework>net10.0</TargetFramework>` y usa la plantilla moderna de Blazor
("Blazor Web App", la que reemplazó a `blazorserver` desde .NET 8), que es 100% compatible con
todos los conceptos de los PDFs (Razor, `@bind`, `EventCallback`, `EditForm`, CSS Isolation, JS
Interop, etc.). La única diferencia real es la estructura de carpetas: en vez de `Pages/` y
`Shared/` sueltos en la raíz, todo vive dentro de `Components/` (`Components/Pages`,
`Components/Layout`, `Components/Shared`), y el render mode interactivo se declara explícitamente
en `App.razor` / `Program.cs`. Si tu profesor pide literalmente `net8.0`, alcanza con cambiar esa
línea en el `.csproj` y bajar las versiones de los paquetes EF Core a `8.0.x`; el resto del código
no cambia.

## Instalación

```bash
# 1. Restaurar dependencias
dotnet restore

# 2. Instalar la herramienta de EF Core (si no la tenés)
dotnet tool install --global dotnet-ef

# 3. Crear la migración inicial (incluye el seed de categorías y tareas)
dotnet ef migrations add InicialConSeed

# 4. Aplicar la migración y crear la base SQLite
dotnet ef database update

# 5. Ejecutar la aplicación
dotnet run
```

> Los pasos 3 y 4 no se pudieron ejecutar automáticamente al generar este proyecto porque el
> entorno donde se creó no tiene acceso a NuGet. Son exactamente los mismos comandos que la Clase
> 16 de la Unidad 3 te pide practicar, así que también te sirven como parte de la entrega.

## Estructura del proyecto

```
GestorTareasIntegrador/
├── Components/
│   ├── App.razor, Routes.razor, _Imports.razor
│   ├── Layout/        (MainLayout, NavMenu)
│   ├── Pages/          (Tareas, NuevaTarea, EditarTarea, TareaDetalle, Error)
│   └── Shared/         (TareaItem, Paginacion, AlertError, ConfirmDialog,
│                          EstadisticasBar, FiltroTareas, DarkModeToggle)
├── Data/               (TareasDbContext)
├── Models/             (TareaEntity, CategoriaEntity, PagedResult, EstadisticasDto)
├── Services/            (ITareaService/TareaService, ICategoriaService/CategoriaService)
├── State/              (TareasState — servicio reactivo Scoped)
├── wwwroot/
│   ├── css/app.css      (variables CSS, modo oscuro)
│   └── js/interop.js    (módulo ES6 para JS Interop)
└── .github/workflows/dotnet.yml
```

## Checklist de requisitos cubiertos

**Unidad 1 — Componentes Blazor**
- [x] 7 componentes reutilizables y encapsulados (`TareaItem`, `Paginacion`, `AlertError`,
      `ConfirmDialog`, `EstadisticasBar`, `FiltroTareas`, `DarkModeToggle`)
- [x] `EditForm` + `DataAnnotationsValidator` + validación cruzada con `IValidatableObject`
- [x] Routing con parámetros (`/tareas/{FiltroEstado}`, `/tarea/{Id:int}`, `/editar-tarea/{Id:int}`)
      y navegación programática (`NavigationManager.NavigateTo`)
- [x] `TareasState` como servicio Scoped con notificación reactiva (`event Action? OnChange`)

**Unidad 2 — Interfaz visual**
- [x] HTML5 semántico (`header`, `nav`, `main`, `article`, `footer`) en el layout
- [x] CSS Isolation en `TareaItem.razor.css`, `ConfirmDialog.razor.css`, `EstadisticasBar.razor.css`
- [x] Bootstrap 5 responsive, mobile-first (`col-12 col-lg-6 col-xxl-4`)
- [x] JS Interop con módulo ES6, `IJSObjectReference` y `DisposeAsync` (modo oscuro + portapapeles)
- [x] Modal de confirmación sin JS de Bootstrap (HTML + CSS puro)

**Unidad 3 — Persistencia**
- [x] EF Core + SQLite con `IDbContextFactory<TareasDbContext>`
- [x] `TareaEntity` y `CategoriaEntity` con relación uno-a-muchos (Fluent API)
- [x] Migraciones (a generar con `dotnet ef migrations add`) + datos de seed
- [x] CRUD completo, paginación (`Skip`/`Take`) y búsqueda (`Contains` → `LIKE`)
- [x] Manejo de errores con `try/catch` + `AlertError` en la UI

**Unidad 4 — Prácticas profesionales**
- [x] `.gitignore` para .NET
- [x] Workflow de GitHub Actions (`dotnet.yml`) con restore + build en cada push
- [x] README profesional + CHANGELOG con historial de versiones
- [ ] Deploy a Azure App Service (paso manual: crear el App Service, agregar el secret
      `AZURE_WEBAPP_PUBLISH_PROFILE` y descomentar el step de deploy en el workflow)

## Flujo de trabajo Git sugerido

```bash
git init
git add .
git commit -m "feat: proyecto Blazor integrador con EF Core y SQLite"
git branch -M main
git remote add origin https://github.com/tu-usuario/gestor-tareas-integrador.git
git push -u origin main

git checkout -b feature/nueva-funcionalidad
# ... cambios ...
git commit -m "feat: descripción del cambio"
git push origin feature/nueva-funcionalidad
# Abrir Pull Request en GitHub y hacer merge
```
