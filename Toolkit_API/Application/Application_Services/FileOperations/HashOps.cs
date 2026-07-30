using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.Infrastructure.Repositories;

namespace Toolkit_API.Application.Application_Services.FileOperations
{
    public class HashOps
    {
        private readonly IFileHasher _fileHasher;
        private readonly IFileScanRepo _fileScanRepo;
        public HashOps(IFileHasher fileHasher, IFileScanRepo fileScanRepo)
        {
            _fileHasher = fileHasher;
            _fileScanRepo = fileScanRepo;
        }

        public async Task<FileScanLog> ComputeFileHashAsync(string filePath, int userId)
        {
            var hashBytes = await _fileHasher.HashFileAsync(filePath);
            var hashExists = await _fileScanRepo.DoubleHash(hashBytes);

            if (hashExists != null)
            {
                var file = await _fileScanRepo.GetFile(hashBytes, userId); 
                return file;
            }
            return null;
            
        }
    }
}
