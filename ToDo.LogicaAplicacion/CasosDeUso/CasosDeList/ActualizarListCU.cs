using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using TodoApi.Dtos.List;

namespace ToDo.LogicaAplicacion.CasosDeUso.CasosDeList;

public interface IActualizarListCU
{
    Task<List?> EjecutarAsync(long id, UpdateListDTO dto);
}

public class ActualizarListCU : IActualizarListCU
{
    private readonly IListRepositorio _listRepositorio;

    public ActualizarListCU(IListRepositorio listRepositorio)
    {
        _listRepositorio = listRepositorio;
    }

    public async Task<List?> EjecutarAsync(long id, UpdateListDTO dto)
    {
        var list = await _listRepositorio.ObtenerPorIdAsync(id);
        
        if (list == null)
        {
            return null;
        }

        list.Name = dto.Name;
        await _listRepositorio.ActualizarAsync(list);

        return list;
    }
}
