using Microsoft.AspNetCore.Http;

namespace E_Commerce.Application.Common.Helpers.Interfaces
{
    public interface IFileHelper
    {       
        string? UploadFile(IFormFile file, string folder);
        bool DeleteFile(string imageUrl, string Folder);
    }
}
