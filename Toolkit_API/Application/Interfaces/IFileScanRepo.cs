namespace Toolkit_API.Application.Interfaces
{
    public interface IFileScanRepo
    {
        public Task<byte[]> HashFile(string filePath, int userId);
    }
}
