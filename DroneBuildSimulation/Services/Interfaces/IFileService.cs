using Microsoft.AspNetCore.Http;

namespace DroneBuildSimulation.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
        void DeleteFile(string fileName, string folderName);
    }
}