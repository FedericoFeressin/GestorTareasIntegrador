# Changelog

Formato basado en [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/).

## [0.10.0] - Campanita de notificaciones en la navbar
### Agregado
- Nuevo componente reutilizable `CampanaNotificaciones` (`Components/Shared/CampanaNotificaciones.razor`)
  en la navbar: ícono `bi bi-bell` con **badge de conteo** y desplegable que lista las tareas
  pendientes que vencen en **menos de 24 hs** (título + fecha/hora), calculadas sobre **todas** las
  tareas (`TareasState.ObtenerTodasAsync`) y actualizadas con `OnChange` ante cada
  crear/editar/eliminar/toggle.
- Botón **"Activar notificaciones del navegador"** dentro del desplegable (visible solo si el
  permiso está en `default`): solicita el permiso `Notification` **dentro de un clic** (user
  gesture), que es la única forma que Edge/Brave/Chrome aceptan sin colgarse.
- Funciones JS `solicitarPermisoNotificaciones()` y `obtenerPermisoNotificaciones()` en
  `wwwroot/js/interop.js`.

### Arreglado
- **Fallo de carga en Edge/Brave:** la notificación se disparaba dentro de `Cargar()` y
  `Notification.requestPermission()` sin gesto del usuario podía quedarse colgado en un prompt
  silencioso, dejando las tarjetas "cargando" para siempre. Ahora la página **nunca** espera por
  las notificaciones: el aviso vive solo en la campanita y `mostrarNotificacion` solo actúa si el
  permiso ya fue concedido.
- El criterio de vencimiento pasó de "vence hoy o mañana sobre la página actual" a un cálculo real
  de **menos de 24 hs** (`DateTime.Now.AddHours(24)`) evaluando todas las tareas, no solo las 6 de
  la página visible.

### Cambiado
- La notificación nativa ahora se muestra **una sola vez por sesión** (direccionada desde la
  campanita) y solo si el permiso ya está concedido; el primer pedido de permiso siempre lo hace el
  usuario con el botón.
- `Tareas.razor` ya no contiene lógica de notificaciones (`NotificarVencimientosProximos`,
  `DisposeAsync`, `_modulo`): la página vuelve a un ciclo de carga simple y sin dependencias JS.

## [0.9.0] - Notificaciones de tareas por vencer
### Agregado
- Notificaciones del navegador (API `Notification`) para tareas pendientes que vencen en
  menos de 24 hs (hoy o mañana), según el requisito de la Unidad 2 (Clase 13, lab. 19).
- Nueva función `mostrarNotificacion(titulo, mensaje)` en `wwwroot/js/interop.js:21` que
  solicita permiso la primera vez y muestra la notificación si está concedido.
- El listado (`Tareas.razor:160`) evalúa las tareas cargadas y avisa **una sola vez por sesión**
  (`notificacionesMostradas`), sin interrumpir la carga. El módulo ES6 se libera con
  `DisposeAsync` ante `JSDisconnectedException`.

### Documentación
- XML docs (`<summary>`, `<param>`, `<returns>`, `<exception>`) en `ITareaService.cs` y
  `ICategoriaService.cs` (objetivo de la Clase 21).
- README con sección de **Conventional Commits** y Git Flow (tipos `feat/fix/docs/...` y ramas
  `main/develop/feature/hotfix`).

## [0.8.0] - Pulido responsive y contraste del navbar
### Arreglado
- Botones "Guardar/Cancelar" y "Guardar cambios/Cancelar" ahora apilan en ancho completo en
  mobile (`flex-column flex-sm-row`) en `NuevaTarea` y `EditarTarea`: ya no quedan apretados
  en pantallas chicas.
- Botón hamburguesa del navbar con más contraste sobre el gradiente índigo: borde translúcido
  claro, fondo al hover y halo de foco (`app.css` `.navbar-toggler`).

## [0.7.0] - Validaciones de backend reforzadas
### Agregado
- Regla `[RegularExpression("^(Alta|Media|Baja)$")]` en `TareaEntity.Prioridad`: cualquier valor
  distinto a Alta/Media/Baja se rechaza con mensaje claro (`TareaEntity.cs:22`).
- Regla `[RegularExpression(@"^\S(?:.*\S)?$")]` en `TareaEntity.Titulo`: el título no puede
  comenzar ni terminar con espacios (`TareaEntity.cs:15`).
- Validación de título con solo espacios en `Validate()`: `"   "` ya no pasa el `[Required]`
  (`TareaEntity.cs:38-43`).
- Re-validación del modelo en la capa de servicio: `Crear` y `Actualizar` llaman a un `Validar()`
  privado que usa `Validator.TryValidateObject` y lanza `ArgumentException` si el modelo es inválido
  (defensa en profundidad, `TareaService.cs:96-105`).
- `ValidationMessage` de Descripción en `EditarTarea` (antes solo existía en `NuevaTarea`).

### Seguridad/robustez
- `ObtenerPaginado` ahora clampea `pagina = Math.Max(1, pagina)` y `tamanioPagina` entre 1 y 100:
  un valor `0` o negativo ya no puede romper `Skip`/`Take` (`TareaService.cs:109-110`).

## [0.6.0] - Rediseño visual índigo y layout
### Cambiado
- Nueva paleta de colores Índigo/Violeta (`wwwroot/css/app.css`): primario `#6366f1`,
  hover `#4f46e5`, acento `#8b5cf6`, fondo claro `#f1f5f9`, fondo oscuro `#17151f`.
- Fuente **Inter** (400-700) cargada desde Google Fonts y aplicada al `body` (`App.razor:9-11`).
- Navbar con gradiente índigo (`.navbar-primario`) que ahora ocupa el 100% del ancho de la página
  (se eliminó el padding lateral del `<header>` en `MainLayout.razor`).
- El contenido principal se envuelve en `container-xl py-4`: ancho máximo centrado en pantallas
  grandes y aire vertical consistente (`MainLayout.razor:9`).
- Headings `h1`/`h2` estandarizados con Inter (peso 600, letter-spacing, margen inferior 1.5rem).
- Filtros responsive (`FiltroTareas.razor`): en mobile cada filtro ocupa `col-12` (se apilan
  buscador, prioridad y botón en líneas completas).
- Grilla de tarjetas con gutter consistente `g-3` (`Tareas.razor`) y paginación separada de las
  tarjetas con `mt-4` (`Paginacion.razor`).

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
