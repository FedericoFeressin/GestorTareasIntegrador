# Changelog

Formato basado en [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/).

## [0.5.0] - Fix de UX en detalle y edición
### Arreglado
- El mensaje "¡Copiado!" en `TareaDetalle` ahora se oculta solo tras 2 segundos (antes quedaba
  visible permanentemente). Se usa un `Timer` que se descarta en cada click y se libera en
  `DisposeAsync`.
- Botón "Guardar cambios" en `EditarTarea` ahora se deshabilita durante el guardado (doble click
  ya no puede disparar dos updates). Misma lógica de `guardando` que ya tenía `NuevaTarea`.

## [0.4.0] - Corrección de bugs y modo oscuro
### Arreglado
- Menú hamburguesa no funcionaba en mobile: se agregó Bootstrap JS bundle en `App.razor`.
- Modo oscuro incompleto: headings, descripciones, botones, badges, paginación, labels,
  footer y bordes ahora se ven correctamente al alternar el tema.
- Comentario TODO pendiente eliminado de `NavMenu.razor`.

### Agregado
- `GUIA_USUARIO.md` con instrucciones de uso para el usuario final.
- `GUIA_TECNICA.md` con tabla de componentes, líneas clave y conceptos de cátedra para evaluación.

## [0.3.0] - Unidad 4
### Agregado
- Workflow de GitHub Actions (`dotnet.yml`) para build automático en cada push.
- README profesional con instrucciones de instalación y stack tecnológico.
- CHANGELOG con historial de versiones.

## [0.2.0] - Unidad 3
### Agregado
- `TareasDbContext` con EF Core y SQLite, entidades `TareaEntity` y `CategoriaEntity` relacionadas.
- `ITareaService` / `TareaService` con CRUD, paginación, búsqueda y estadísticas.
- Datos de seed (5 categorías, 10 tareas).
### Cambiado
- El listado de tareas pasó de vivir en memoria a persistir en SQLite.

## [0.1.0] - Unidades 1 y 2
### Agregado
- Estructura inicial del proyecto Blazor Server (.NET 10, plantilla Blazor Web App).
- Componentes reutilizables: `TareaItem`, `Paginacion`, `AlertError`, `ConfirmDialog`,
  `EstadisticasBar`, `FiltroTareas`, `DarkModeToggle`.
- Formularios con `EditForm`, `DataAnnotationsValidator` y validación cruzada (`IValidatableObject`).
- Routing con parámetros (`/tareas/{estado}`, `/tarea/{id}`, `/editar-tarea/{id}`).
- CSS Isolation, HTML5 semántico, grilla responsive con Bootstrap 5.
- JS Interop: modo oscuro persistente y copiar al portapapeles.
