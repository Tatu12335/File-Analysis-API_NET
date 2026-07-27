namespace Toolkit_API.Application.Interfaces
{
    public interface IFileHasher
    {
        public Task<FileStream> OpenFS(string filePath);
        public Task<byte[]> HashFileAsync(string filePath);
    }
}
