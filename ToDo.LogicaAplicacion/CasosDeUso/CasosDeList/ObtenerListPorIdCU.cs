using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

namespace ToDo.LogicaAplicacion.CasosDeUso.CasosDeList;

public interface IObtenerListPorIdCU
{
    Task<List?> EjecutarAsync(long id);
}

public class ObtenerListPorIdCU : IObtenerListPorIdCU
{
    private readonly IListRepositorio _listRepositorio;

    public ObtenerListPorIdCU(IListRepositorio listRepositorio)
    {
        _listRepositorio = listRepositorio;
    }

    public async Task<List?> EjecutarAsync(long id)
    {
        return await _listRepositorio.ObtenerPorIdAsync(id);
    }
}
