using Toolkit_API.Application.Interfaces;

namespace Toolkit_API.Application.Analysis
{
    public class ScoringAlg
    {
        private const int MaxScore = 100;
        private const int MinScore = 0;

        private readonly IFileAnalysis _fileAnalysis;
        private double _score;
        public ScoringAlg(IFileAnalysis fileAnalysis, double score)
        {
            _fileAnalysis = fileAnalysis;
            this._score = score;
        }

        public async Task <double> CalculateScore(string filepath)
        {

            var extensionMatches = await _fileAnalysis.ExtensionMatches(filepath);
            
            if(!extensionMatches)
                _score += 20.0; // Penalty for extension mismatch
            switch(_score)
            {
                case > MaxScore:
                    _score = MaxScore;
                    break;
                
                case < MinScore:
                    _score = MinScore;
                    break;
            }


            return _score;
        }
    }
}
