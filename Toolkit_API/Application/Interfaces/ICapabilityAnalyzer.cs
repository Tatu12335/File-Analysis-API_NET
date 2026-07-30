using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Application.Interfaces
{
    public interface ICapabilityAnalyzer
    {
        public Task<DetectionResult> AnalyzeCapabilities(ScanResult scanResult);
    }
}
