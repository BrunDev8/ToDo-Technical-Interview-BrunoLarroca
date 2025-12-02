using Microsoft.EntityFrameworkCore;
using Dominio.InterfacesRepositorio;
using AccesoDatos.Data;

namespace AccesoDatos.Repositorios;

public class Repositorio<T> : IRepositorio<T> where T : class
{
    protected readonly TodoContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repositorio(TodoContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> ObtenerTodosAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> ObtenerPorIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T> AgregarAsync(T entidad)
    {
        await _dbSet.AddAsync(entidad);
        await _context.SaveChangesAsync();
        return entidad;
    }

    public virtual async Task ActualizarAsync(T entidad)
    {
        _dbSet.Update(entidad);
        await _context.SaveChangesAsync();
    }

    public virtual async Task EliminarAsync(T entidad)
    {
        _dbSet.Remove(entidad);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<bool> ExisteAsync(long id)
    {
        var entidad = await _dbSet.FindAsync(id);
        return entidad != null;
    }
}
