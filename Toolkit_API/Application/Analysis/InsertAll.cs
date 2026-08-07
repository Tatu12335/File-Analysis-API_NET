using Toolkit_API.Application.Interfaces;

namespace Toolkit_API.Application.Analysis
{
    public class InsertAll
    {
        private readonly IFileScanRepo _fileScanRepository;
        
        public InsertAll(IFileScanRepo fileScanRepository)
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
