using System.Diagnostics;
using Toolkit_API.Application.Interfaces;
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
        public async Task DetermineMagicBytes(byte[] bytes)
        {
            var magicBytes = bytes.Take(4).ToArray();
            Debug.WriteLine($"Magic Bytes: {BitConverter.ToString(magicBytes)}");
        }
        public async Task<string> AnalyzeFile(string filePath)
        {
            var bytes = await File.ReadAllBytesAsync(filePath);
            var fileType = await Detect(bytes);
            await DetermineMagicBytes(bytes);
            Debug.WriteLine($"File Type Detected: {fileType}");
            return fileType;
        }
    }
}
