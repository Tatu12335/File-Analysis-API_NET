namespace Toolkit_API.Domain.Entities.Files
{
    public class DetectionResult
    {
        public string RuleName { get; set; }
        public double Score { get; set; }
        public double Confidence { get; set; }
        public string Description { get; set; }
    }
}
