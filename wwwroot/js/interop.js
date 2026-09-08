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
export async function mostrarNotificacion(titulo, mensaje) {
    if (!('Notification' in window)) return false;
    let permiso = Notification.permission;
    if (permiso === 'default') {
        permiso = await Notification.requestPermission();
    }
    if (permiso === 'granted') {
        new Notification(titulo, { body: mensaje });
        return true;
    }
    return false;
}
