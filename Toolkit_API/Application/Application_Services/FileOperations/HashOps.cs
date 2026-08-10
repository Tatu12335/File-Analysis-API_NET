using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.Files;

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

                if (file == null)
                {
                    // if the file does not exist for the user, create a new FileScanLog object and return it
                    return new FileScanLog
                    {
                        FileHash = hashBytes,
                        FileName = Path.GetFileName(filePath),
                        Score = 0
                    };
                }

                return new FileScanLog
                {
                    Capability = file.Capability,
                    FileHash = file.FileHash,
                    FileName = file.FileName,
                    Score = file.Score
                };
            }
            // incase the hash does not exist, create a new FileScanLog object and return it
            return new FileScanLog
            {
                FileHash = hashBytes,
                FileName = Path.GetFileName(filePath),
                Score = 0
            };

        }
    }
}
