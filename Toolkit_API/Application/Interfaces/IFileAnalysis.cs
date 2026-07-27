using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Entities.Files;
namespace Toolkit_API.Application.Interfaces
{
    public interface IFileAnalysis
    {
        public Task<string> Detect(byte[] bytes);
        public Task<string> AnalyzeFile(string filePath);
        public Task<DetectionResult> ExtensionMatches(string filepath);
        public Task<List<DetectionResult>> ComboDetection(string filePath, ExtractedStrings.ComboRule comboRule, ExtractedStrings extractedStrings);
        
    }
}
