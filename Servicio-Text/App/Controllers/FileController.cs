using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using App.Domain.Interface;
using App.Domain.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace App.Controllers
{
    /// <summary>
    /// La clase anterior es una clase de controlador de C# llamada FileController que maneja solicitudes
    /// de API relacionadas con archivos.
    /// </summary>
        [Route("api/[controller]")]
    [ApiController]
    
    public class FileController : ControllerBase
    {
        private readonly IFileConvert _fileConvert;
        public FileController(IFileConvert fileConvert)
        {
            _fileConvert = fileConvert;
        }
        /// <summary>
        /// La función `UploadFile` en un controlador C# maneja las operaciones de carga, conversión,
        /// serialización y deserialización de archivos.
        /// </summary>
        /// <param name="file">`IFormFile` es un tipo en ASP.NET Core que representa un archivo
        /// enviado con HttpRequest. En el fragmento de código proporcionado, el método de acción
        /// "UploadFile" espera que se cargue un archivo como parte de la solicitud. El parámetro `file`
        /// de tipo `IFormFile`.</param>
        /// <param name="numberOfObjects">El parámetro `numberOfObjects` en el método `UploadFile` es un
        /// número entero que especifica la cantidad de objetos que se crearán o procesarán en función
        /// del archivo cargado. Este parámetro se utiliza en el método para determinar cuántos objetos
        /// se deben crear o procesar durante el proceso de conversión de archivos. Ayuda a
        /// controlar.</param>
        /// <returns>
        /// El método `UploadFile` devuelve una respuesta BadRequest con un mensaje "El archivo está
        /// vacío" si el archivo es nulo, está vacío o no es de tipo "text/plain", o devuelve una
        /// respuesta Ok con los datos deserializados en el formulario de una lista de objetos
        /// `AppRegistro` si el archivo se procesa exitosamente. Si ocurre una excepción durante el
        /// procesamiento, devuelve un StatusCode 500.
        /// </returns>
        /// <response code="200">Retorna la lista de objetos convertidos.</response>
        /// <response code="400">El archivo está vacío o tiene un formato no soportado.</response>
        /// <response code="500">Ocurrió un error durante el procesamiento del archivo.</response>
        [HttpPost("UploadFile")]
        [ProducesResponseType(typeof(List<AppRegistro>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult UploadFile(IFormFile file, int numberOfObjects)
        {
            if (file == null || file.Length == 0 || file.ContentType != "text/plain")
            {
                return BadRequest("File is empty");
            }
            try
            {
                List<AppRegistro>? data = _fileConvert.ConvertirData(file, numberOfObjects);
                string jsonContent = JsonConvert.SerializeObject(data);
                List<AppRegistro>? deserializedData = JsonConvert.DeserializeObject<List<AppRegistro>>(jsonContent);
                return Ok(deserializedData);
            }
            catch (Exception ex)
            {
                ResponseRespuesta res = new ResponseRespuesta()
                {
                    Codigo = "400",
                    Mensaje = "Ocurrio un incoveniente"
                };
                return StatusCode(400,res);
            }
        }
    }
}