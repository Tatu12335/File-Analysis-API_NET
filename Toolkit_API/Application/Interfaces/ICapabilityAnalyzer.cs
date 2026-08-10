using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Application.Interfaces
{
    public interface ICapabilityAnalyzer
    {
        //public Task <ScanResult> AnalyzeCapabilities(DetectionResult scanResult);
        public IEnumerable<Capability> GetCapabilitiesName(ReadOnlySpan<byte> pattern);
    }
}
