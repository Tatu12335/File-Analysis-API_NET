using Toolkit_API.Domain.Entities.Files;

namespace Toolkit_API.Application.Calculators
{
    public class ScoringAlgorithmn
    {
        public double CalculateScore(double confidence, double severity)
        {

            double score = confidence * 0.6 + severity * 0.4;
            score = Math.Round(score, 2);
            return score;
        }
    }
}
