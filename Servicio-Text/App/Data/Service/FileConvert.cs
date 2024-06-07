using App.Domain.Interface;
using App.Domain.Models;
using System;

namespace App.Data.Service;

public class FileConvert : IFileConvert
{
    public List<AppRegistro> ConvertirData(IFormFile file, int numberOfObjects)
    {
        List<AppRegistro> data = new List<AppRegistro>();
        StreamReader? reader = new StreamReader(file.OpenReadStream());
        string? linea;
        while ((linea = reader?.ReadLine()) != null && data.Count < numberOfObjects)
        {
            List<string> parts = linea.Split(';').Select(p => p.Trim()).ToList();

            if (linea.StartsWith("D110"))
            {
                string DNI = linea.Substring(62, 8);
                string completeName = EliminarSpaciado(linea.Substring(74, 116));
                string year = linea.Substring(548, 4);
                string month = linea.Substring(552, 2);
                string day = linea.Substring(554, 2);
                string fechaPago = $"{year}/{month}/{day}";
                string totalCobro = linea.Substring(524, 16);

                var AppRegistro = new AppRegistro
                {
                    Dni = DNI,
                    NombreCompleto = completeName,
                    Fecha = fechaPago,
                    CobroTotal = totalCobro
                };
                data.Add(AppRegistro);
            }
        }
        return data;
    }
    private string EliminarSpaciado(string cadena)
    {
        string[] palabras = cadena.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return string.Join(" ", palabras);
    }
}