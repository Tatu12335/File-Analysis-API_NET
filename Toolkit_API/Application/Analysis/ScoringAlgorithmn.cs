using Toolkit_API.Domain.Entities.Files;

namespace Toolkit_API.Application.Analysis
{
    public class ScoringAlgorithmn
    {
        public double CalculateScore(ScanResult scanResult)
        {

            double score = (scanResult.confidence * 0.6) + (scanResult.severity * 0.4);
            score = Math.Round(score, 2);
            return score;
        }
    }
}
