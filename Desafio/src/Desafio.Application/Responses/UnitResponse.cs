using Desafio.Domain;
using System.Collections.Generic;

namespace Desafio.Application;

public class UnitResponse
{
    public bool Success { get; set; }
    public Unit Unit { get; set; }
    public List<string> Errors { get; set; }

    //Inicializa Lista de Erros
    public UnitResponse()
    {
        Errors = new List<string>();
    }
    //Construtor
    public UnitResponse(bool success = true, Unit unit = null) : this()
    {
        Success = success;
        Unit = unit;
    }

    //Adicionar lista de erros se tiver
    public void InsertErrors(IEnumerable<string> errors)
    {
        Errors.AddRange(errors);
    }
    public void InsertError(string error)
    {
        Errors.Add(error);
    }
}
