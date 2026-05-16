namespace Toolkit_API.Application.Interfaces
{
    public interface IHandleUploadFolder
    {
        public Task<string> SaveFileToUploadFolder(IFormFile file);
        public Task<string> GetFilePath(string fileName);
        public Task ReadFile(string filePath);
        public Task CreateUploadFolder();
    }
}
