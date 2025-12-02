using Dominio.InterfacesRepositorio;

namespace ToDo.LogicaAplicacion.CasosDeUso.CasosDeList;

public interface IEliminarListCU
{
    Task<bool> EjecutarAsync(long id);
}

public class EliminarListCU : IEliminarListCU
{
    private readonly IListRepositorio _listRepositorio;

    public EliminarListCU(IListRepositorio listRepositorio)
    {
        _listRepositorio = listRepositorio;
    }

    public async Task<bool> EjecutarAsync(long id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("El ID debe ser mayor a 0.", nameof(id));
        }

        var list = await _listRepositorio.ObtenerPorIdAsync(id);
        
        if (list == null)
        {
            return false;
        }

        await _listRepositorio.EliminarAsync(list);
        return true;
    }
}
