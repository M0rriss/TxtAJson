using App.Domain.Models;

namespace App.Domain.Interface;

public interface IFileConvert
{
    List<AppRegistro> ConvertirData(IFormFile file, int numberOfObjects); 
}