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
