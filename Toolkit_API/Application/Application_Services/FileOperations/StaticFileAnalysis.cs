using Toolkit_API.Application.Analysis;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Entities.Files;

namespace Toolkit_API.Application.Application_Services.Operations
{
    public class StaticFileAnalysis
    {
        private readonly IFileAnalysis _fileAnalysis;
        private readonly ScoringAlg _scoringAlg;
        private readonly ExtractedStrings _extractedStrings;
        private readonly ExtractedStrings.ComboRule _comboRule;


        public StaticFileAnalysis(IFileAnalysis fileAnalysis, ScoringAlg scoringAlg, ExtractedStrings extractedStrings, ExtractedStrings.ComboRule comboRule)
        {
            _fileAnalysis = fileAnalysis;
            _scoringAlg = scoringAlg;
            _extractedStrings = extractedStrings;
            _comboRule = comboRule;

        }
        public async Task<DetectionResult> AnalyzeFile(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException();
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            var analysisResult = await _fileAnalysis.AnalyzeFile(filePath);
            var extensionMatch = await _fileAnalysis.ExtensionMatches(filePath);
            var opcodeScore  = await _fileAnalysis.ComboDetection(filePath, _comboRule  ,_extractedStrings);


            var score = await _scoringAlg.CalculateScore(new List<DetectionResult>
            {
                extensionMatch,
                opcodeScore
            });

            return new DetectionResult
            {
                RuleName = "Static Analysis",
                Score = score,
                Confidence = 1.0


            };
        }
    }
}
