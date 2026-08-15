using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Policies;
namespace Toolkit_API.Domain.Entities.Files
{
    public enum RiskLevel { Low, Medium, High, Critical }
    public class ScanResult
    {
        public double score { get; set; }
        public RiskLevel riskLevel { get; set; } = RiskLevel.Low;
        public IEnumerable<Capability> capabilities { get; set; } = new List<Capability>();
        public byte[] fileHash { get; set; }
        public string fileName { get; set; }
        public int isMalwareBazaarMatch { get; set; } // INT Beacause of sql server , 0 = false, 1 = true
        public double confidence { get; set; } = 0.0;
        public double severity { get; set; } = 0.0;
        public IEnumerable<DetectionSource> detectionSource { get; set; }    
    }
}
