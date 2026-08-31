# Guía de usuario — Gestor de Tareas

## 1. Iniciar la aplicación

```bash
dotnet run
```

Abrí en el navegador la dirección que aparezca en consola (generalmente `https://localhost:5001`
o `http://localhost:5000`).

## 2. Panel principal (Listado de Tareas)

Al ingresar se muestra el listado de tareas con:

- **Barra de estadísticas** — Total, completadas, pendientes y prioridad alta.
- **Filtros** — Buscador por título, filtro por prioridad (Todas/Alta/Media/Baja) y botón
  Limpiar.
- **Tarjetas de tareas** — Cada tarjeta muestra título, descripción, fecha de vencimiento,
  categoría y prioridad.
- **Paginación** — Navegación entre páginas si hay más de 6 tareas.

## 3. Crear una tarea

1. Click en **"+ Nueva tarea"** en la barra de navegación.
2. Completar el formulario:
   - **Título** (obligatorio, 3-100 caracteres)
   - **Descripción** (opcional, máx. 500 caracteres)
   - **Prioridad** (Alta / Media / Baja)
   - **Fecha de vencimiento** (obligatoria, no puede ser en el pasado para tareas pendientes)
   - **Categoría** (opcional)
3. Click en **"Guardar"**. Se redirige al listado.

## 4. Editar una tarea

1. En la tarjeta de la tarea, click en **"Editar"**.
2. Modificar los campos necesarios.
3. Marcar **"Completada"** si corresponde.
4. Click en **"Guardar cambios"**.

## 5. Completar / Reabrir una tarea

En la tarjeta, click en **"Completar"** o **"Reabrir"**. La tarea cambia de estado
instantáneamente sin recargar la página.

## 6. Eliminar una tarea

1. En la tarjeta, click en **"Eliminar"**.
2. Aparece un modal de confirmación. Click en **"Eliminar"** para confirmar o **"Cancelar"**
   para volver.

## 7. Ver detalle de una tarea

Click en **"Detalle"** en la tarjeta. Se muestra una vista ampliada con toda la información
y un botón **"Copiar enlace"** para compartir la URL de la tarea.

## 8. Filtrar tareas

- **Por estado:** Usar los links "Pendientes" o "Completadas" en la barra de navegación.
- **Por prioridad:** Usar el select de prioridad en los filtros.
- **Por título:** Escribir en el buscador (búsqueda en tiempo real).
- **Limpiar:** Botón "Limpiar" para resetear todos los filtros.

## 9. Modo oscuro

Click en el ícono de luna/soleado (esquina superior derecha del navbar) para alternar
entre tema claro y oscuro. La preferencia se guarda automáticamente en `localStorage`
y persiste al recargar la página.

## 10. Navegación desde mobile

En pantallas chicas, el menú de navegación se colapsa detrás de un botón hamburguesa.
Tocar el ícono para expandir las opciones.
