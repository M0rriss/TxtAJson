namespace App.Domain.Models;

public class ResponseRespuesta
{
    private string _mensaje;
    private string _codigo;
    public string Mensaje { get => _mensaje; set => _mensaje = value; }
    public string Codigo { get => _codigo; set => _codigo = value; }

    public ResponseRespuesta()
    {
        _codigo = "";
        _mensaje = "";
    }
}