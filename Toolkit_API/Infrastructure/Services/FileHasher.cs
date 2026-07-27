using System.Security.Cryptography;
using Toolkit_API.Application.Interfaces;

namespace Toolkit_API.Infrastructure.Services
{
    public class FileHasher : IFileHasher
    {
        public async Task<FileStream> OpenFS(string filePath)
        {
            return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }
        public async Task<byte[]> HashFileAsync(string filePath)
        {
            var sha256 = SHA256.Create();

            using (var stream = await OpenFS(filePath))
            {
                var hashBytes = sha256.ComputeHash(stream);
                return hashBytes;

            }
        }
        public async Task <>
    }
}
