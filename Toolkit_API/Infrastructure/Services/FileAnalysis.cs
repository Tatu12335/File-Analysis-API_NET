using System.Diagnostics;
using System.Text;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Entities.Files;
namespace Toolkit_API.Infrastructure.Services
{
    public class FileAnalysis : IFileAnalysis
    {
        public FileAnalysis()
        {

        }
        public async Task<string> Detect(byte[] bytes) => bytes switch
        {
            [0x4D, 0x5A, ..] => "Executable (PE)",
            [0x25, 0x50, 0x44, 0x46, ..] => "PDF Document",
            [0xFF, 0xD8, 0xFF, ..] => "JPEG Image",
            [0x89, 0x50, 0x4E, 0x47, ..] => "PNG Image",
            [0x47, 0x49, 0x46, 0x38, ..] => "GIF Image",
            [0x52, 0x61, 0x72, 0x21, ..] => "RAR Archive",
            [0x50, 0x4B, 0x03, 0x04, ..] => "ZIP Archive",
            _ => "Unknown File Type"
        };

        public async Task<string> AnalyzeFile(string filePath)
        {
            var bytes = await File.ReadAllBytesAsync(filePath);
            var fileType = await Detect(bytes);

            return fileType;
        }
        public async Task<DetectionResult> ExtensionMatches(string filepath)
        {
            if (!File.Exists(filepath))
                throw new FileNotFoundException($"File not found: {filepath}");

            var extension = Path.GetExtension(filepath);
            var bytes = await File.ReadAllBytesAsync(filepath);
            var detectedType = await Detect(bytes);

            if (!detectedType.Contains(extension.TrimStart('.'), StringComparison.OrdinalIgnoreCase))
            {
                return new DetectionResult
                {
                    RuleName = "Extension Mismatch",
                    Score = 10,
                    Confidence = 1.0
                };
            }
            return new DetectionResult
            {
                RuleName = "Extension Matches",
                Score = 0,
                Confidence = 0.0
            };

        }

        public async Task<List<DetectionResult>> ComboDetection(string filePath, ExtractedStrings extractedStrings)
        {
            

            return new List<DetectionResult>();
        }
        
    }
}
