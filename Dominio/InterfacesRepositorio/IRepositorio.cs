namespace Dominio.InterfacesRepositorio;

public interface IRepositorio<T> where T : class
{
    Task<IEnumerable<T>> ObtenerTodosAsync();
    Task<T?> ObtenerPorIdAsync(long id);
    Task<T> AgregarAsync(T entidad);
    Task ActualizarAsync(T entidad);
    Task EliminarAsync(T entidad);
    Task<bool> ExisteAsync(long id);
}
