// Módulo ES6 cargado bajo demanda vía IJSRuntime.InvokeAsync<IJSObjectReference>
// (Unidad 2, Clase 13 - buena práctica de code splitting).

export function aplicarModoOscuro(activo) {
    document.body.classList.toggle('dark-mode', activo);
}

export function guardarPreferenciaOscura(activo) {
    localStorage.setItem('gestorTareas.darkMode', activo ? '1' : '0');
}

export function obtenerPreferenciaOscura() {
    return localStorage.getItem('gestorTareas.darkMode') === '1';
}

export function copiarAlPortapapeles(texto) {
    return navigator.clipboard.writeText(texto);
}

// Notificación del navegador (Unidad 2, Clase 13 - tareas que vencen en <24 hs).
// Solo muestra si el permiso ya fue concedido: nunca se pide permiso automáticamente,
// porque en Edge/Brave/Chrome requestPermission() sin gesto del usuario puede quedarse
// colgado en un prompt silencioso y bloquear el flujo de la página.
export function mostrarNotificacion(titulo, mensaje) {
    if (!('Notification' in window)) return false;
    if (Notification.permission !== 'granted') return false;
    new Notification(titulo, { body: mensaje });
    return true;
}

// Solicita el permiso de notificaciones. Debe invocarse SIEMPRE desde un clic del
// usuario (user gesture): es la única forma que los navegadores aceptan sin colgarse.
export function solicitarPermisoNotificaciones() {
    if (!('Notification' in window)) return false;
    return Notification.requestPermission().then(perm => perm === 'granted');
}

// Devuelve el estado actual del permiso: 'granted', 'denied' o 'default'.
export function obtenerPermisoNotificaciones() {
    if (!('Notification' in window)) return 'unsupported';
    return Notification.permission;
}
