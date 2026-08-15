using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Application.Interfaces
{
    public interface IDetectionSourceBuilder
    {
        public DetectionSource RegisterFindings(Capability capability, Source source);
        public Task<IEnumerable<DetectionSource>> CreateContext(string filepath, ExtractedStrings extractedStrings);
    }
}
