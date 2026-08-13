using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Application.Analysis
{
    public class Insert
    {
        private readonly IFileScanRepo _fileScanRepository;

        public Insert(IFileScanRepo fileScanRepository)
        {
            _fileScanRepository = fileScanRepository;
        }

        public async Task<byte[]> InsertFile(string filePath, int userId, double score)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return null;
            filePath = Path.GetFullPath(filePath);
            var fileHash = await _fileScanRepository.InsertAll(filePath, userId, score);
            return fileHash;
        }
    }
}
