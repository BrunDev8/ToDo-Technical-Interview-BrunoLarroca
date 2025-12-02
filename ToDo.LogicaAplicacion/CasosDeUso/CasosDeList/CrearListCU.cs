using Dominio.Entidades;
using Dominio.InterfacesRepositorio;
using TodoApi.Dtos.List;

namespace ToDo.LogicaAplicacion.CasosDeUso.CasosDeList;

public interface ICrearListCU
{
    Task<List> EjecutarAsync(CreateListDTO dto);
}

public class CrearListCU : ICrearListCU
{
    private readonly IListRepositorio _listRepositorio;

    public CrearListCU(IListRepositorio listRepositorio)
    {
        _listRepositorio = listRepositorio;
    }

    public async Task<List> EjecutarAsync(CreateListDTO dto)
    {
        var list = new List
        {
            Name = dto.Name
        };

        // Validar antes de guardar
        list.Validar();

        return await _listRepositorio.AgregarAsync(list);
    }
}
