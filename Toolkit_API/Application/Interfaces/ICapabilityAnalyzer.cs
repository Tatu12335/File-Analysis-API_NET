using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Application.Interfaces
{
    public interface ICapabilityAnalyzer
    {
        public Task<Capability> AnalyzeCapabilities(string filePath);
    }
}
