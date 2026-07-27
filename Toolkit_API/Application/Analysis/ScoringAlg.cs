using Toolkit_API.Application.Application_Services.Operations;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Entities.Files;

namespace Toolkit_API.Application.Analysis
{
    public class ScoringAlg
    {
        private const int MaxScore = 100;
        private const int MinScore = 0;


        private readonly DetectionResult _detectionResult;

        public ScoringAlg(DetectionResult result)
        {
            _detectionResult = result;
        }

        public async Task<double> CalculateScore(List<DetectionResult> detectionResults)
        {
            double totalScore = detectionResults
                .Sum(m => m.Confidence);
            return Math.Min(totalScore, MaxScore);
        }
    }
}
