using Toolkit_API.Domain.Entities.FileAnalysis;

namespace Toolkit_API.Domain.Entities.Files
{
    public class Findings
    {
        public DetectionSource Source { get; set; }
        public double severity { get; set; }
        public double confidence { get; set; }
    }
}
