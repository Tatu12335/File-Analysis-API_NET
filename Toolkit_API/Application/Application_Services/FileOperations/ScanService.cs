using Hangfire.Server;
using Toolkit_API.Application.Analysis;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.Files;
namespace Toolkit_API.Application.Application_Services.FileOperations
{
    public class ScanService : IScan
    {
        private readonly IResultRepository _resultRepository;
        private readonly StaticScan _staticScan;
        public ScanService(IResultRepository resultRepository, StaticScan staticScan)
        {
            _resultRepository = resultRepository;
            _staticScan = staticScan;
        }
        public async Task ScanFile(string filePath, int userId, PerformContext context = null!)
        {
            string jobId = context?.BackgroundJob?.Id
                ?? throw new Exception("Job ID not found");
            ScanResult result = await _staticScan.ScanFile(filePath, userId);

            await _resultRepository.SaveResultAsync(jobId, result);

        }
    }
}
