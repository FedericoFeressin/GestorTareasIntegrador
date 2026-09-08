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
// Sigue el ejemplo de la Clase 12: si el permiso no está definido, pide permiso.
// Se hace SIN await a requestPermission() para que, si el navegador tarda o bloquea
// el prompt (Edge/Brave sin gesto del usuario), la página no quede cargando para siempre.
export function mostrarNotificacion(titulo, mensaje) {
    if (!('Notification' in window)) return false;
    if (Notification.permission === 'granted') {
        new Notification(titulo, { body: mensaje });
        return true;
    }
    if (Notification.permission !== 'denied') {
        Notification.requestPermission().then(perm => {
            if (perm === 'granted') new Notification(titulo, { body: mensaje });
        });
    }
    return false;
}
