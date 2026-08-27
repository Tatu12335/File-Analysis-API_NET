using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Entities.Files;

namespace Toolkit_API.Application.Analysis
{
    public class AssembleFindings
    {
        public IEnumerable<Findings> Assemble(IEnumerable<DetectionSource> detections,double severity, double confidence)
        {
            var findings = new List<Findings>();
            foreach(var detection in detections)
            {
                findings.Add(new Findings { confidence = confidence, severity = severity, Source = detection });

            }
           return findings;
        }
    }
}
