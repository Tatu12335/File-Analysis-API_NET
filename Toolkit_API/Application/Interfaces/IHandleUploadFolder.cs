namespace Toolkit_API.Application.Interfaces
{
    public interface IHandleUploadFolder
    {
        public Task<string> SaveFileToUploadFolder(string file);
        public Task<string> GetFilePath(string fileName);
        public Task<FileStream> ReadFile(string filePath);
        public Task CreateUploadFolder();
    }
}
