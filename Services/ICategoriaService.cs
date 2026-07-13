using GestorTareasIntegrador.Models;

namespace GestorTareasIntegrador.Services;

public interface ICategoriaService
{
    Task<List<CategoriaEntity>> ObtenerTodas();
}
