using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Application.Interfaces
{
    public interface IDetectionSourceBuilder
    {
        public Task<DetectionSource> RegisterFindings(string filepath, ExtractedStrings extractedStrings);
        public Task CreateContext(string filepath, ExtractedStrings extractedStrings);
    }
}
