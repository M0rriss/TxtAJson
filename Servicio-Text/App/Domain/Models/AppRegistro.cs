using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace App.Domain.Models;


public class AppRegistro
{
    private string _dni;
    private string _nombreCompleto;
    private string _fecha;
    private string _cobroTotal;
    [JsonProperty("DNI")]
    public string Dni { get => _dni; set => _dni = value; }
    public string NombreCompleto { get => _nombreCompleto; set => _nombreCompleto = value; }
    public string Fecha { get => _fecha; set => _fecha = value; }
    public string CobroTotal { get => _cobroTotal; set => _cobroTotal = value; }

    public AppRegistro ()
    {
        _nombreCompleto = "";
        _dni = "";
        _fecha = "";
        _cobroTotal = "";
    }
}