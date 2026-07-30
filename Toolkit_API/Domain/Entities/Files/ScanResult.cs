using Toolkit_API.Domain.Policies;
namespace Toolkit_API.Domain.Entities.Files
{
    public enum RiskLevel { Low, Medium, High, Critical }
    public class ScanResult
    {
        public double score { get; set; }
        public RiskLevel riskLevel { get; set; } = RiskLevel.Low;
        public IEnumerable<Capability> capabilities { get; set; } = new List<Capability>();
        //public IReadOnlyCollection<DetectionResult> detections { get; set; } = new List<DetectionResult>();
    }
}
