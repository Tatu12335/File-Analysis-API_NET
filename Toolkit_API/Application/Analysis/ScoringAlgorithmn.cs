using Toolkit_API.Domain.Entities.Files;

namespace Toolkit_API.Application.Analysis
{
    public class ScoringAlgorithmn
    {
        public double CalculateScore(DetectionResult detectionResult)
        {
            
            double score = (detectionResult.Confidence * 0.6) + (detectionResult.Severity * 0.4);
            score = Math.Round(score, 2);
            return score;
        }
    }
}
