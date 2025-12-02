using Dominio.Entidades;
using Dominio.InterfacesRepositorio;

namespace ToDo.LogicaAplicacion.CasosDeUso.CasosDeList;

public interface IListarListsCU
{
    Task<IEnumerable<List>> EjecutarAsync();
}

public class ListarListsCU : IListarListsCU
{
    private readonly IListRepositorio _listRepositorio;

    public ListarListsCU(IListRepositorio listRepositorio)
    {
        _listRepositorio = listRepositorio;
    }

    public async Task<IEnumerable<List>> EjecutarAsync()
    {
        return await _listRepositorio.ObtenerTodosAsync();
    }
}
