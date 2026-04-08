using System.Diagnostics;
using System.Runtime.InteropServices;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.FileAnalysis;
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
        public byte[] DetermineMagicBytes(byte[] bytes)
        {
            var magicBytes = bytes.Take(4).ToArray();
            return magicBytes;
        }
        public async Task<string> AnalyzeFile(string filePath)
        {
            var bytes = await File.ReadAllBytesAsync(filePath);
            var fileType = await Detect(bytes);

            return fileType;
        }
        public async Task<bool> ExtensionMatches(string filepath)
        {
            if (File.Exists(filepath))
            {
                var extension = Path.GetExtension(filepath);
                var bytes = await File.ReadAllBytesAsync(filepath);
                var detectedType = await Detect(bytes);
                return detectedType.Contains(extension.TrimStart('.'), StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                throw new FileNotFoundException($"File not found: {filepath}");
            }
        }
        public async Task<bool> CheckForSuspiciousPatterns(string filePath,FileAnalysisResult fileAnalysisResult)
        {
            var patterns = new List<byte[]>
            {
                // Common web and protocol patterns
                new byte[] {0x68, 0x74, 0x74, 0x70, 0x3A, 0x2F, 0x2F }, // Common URL pattern
                new byte[] { 0x68, 0x74, 0x74, 0x70, 0x73, 0x3A, 0x2F, 0x2F }, // Common HTTPS URL pattern
                new byte[] {0x55,0x73,0x65,0x72,0x2D,0x41,0x67,0x65,0x6E,0x74}, // "User-Agent" string
                // Suspicious winAPI calls (common in malware)
                new byte[] {0x43, 0x72, 0x65, 0x61, 0x74, 0x65, 0x52, 0x65, 0x6D, 0x6F, 0x74, 0x65, 0x54, 0x68, 0x72, 0x65, 0x61, 0x64}, // "CreateRemoteThread" string
                new byte[] { 0x57, 0x72, 0x69, 0x74, 0x65, 0x50, 0x72, 0x6F, 0x63, 0x65, 0x73, 0x73, 0x4D, 0x65, 0x6D, 0x6F, 0x72, 0x79 }, // "WriteProcessMemory" string
                new byte[] { 0x56, 0x69, 0x72, 0x74, 0x75, 0x61, 0x6C, 0x41, 0x6C, 0x6C, 0x6F, 0x63, 0x45, 0x78 }, // "VirtualAllocEx" string
                new byte[] { 0x53, 0x68, 0x65, 0x6C, 0x6C, 0x45, 0x78, 0x65, 0x63, 0x75, 0x74, 0x65 }, // "ShellExecute" string
                // Common tool calls
                new byte[] { 0x63, 0x6D, 0x64, 0x2E, 0x65, 0x78, 0x65 }, // "cmd.exe" string
                new byte[] { 0x70, 0x77, 0x73, 0x68, 0x2E, 0x65, 0x78, 0x65 }, // "powershell.exe" string
                new byte[] { 0x52, 0x65, 0x67, 0x2D, 0x64, 0x69, 0x74 }, // "Reg-edit" string
                new byte[] { 0x76, 0x73, 0x73, 0x61, 0x64, 0x6D, 0x69, 0x6E } // "vssadmin" string
            };
            var bytes = await File.ReadAllBytesAsync(filePath);
            
            foreach (var pattern in patterns)
            {
                if (bytes.AsSpan().IndexOf(pattern) >= 0)
                {
                    Debug.WriteLine($"Suspicious pattern found in file: {filePath}");
                    fileAnalysisResult.Score += 10; // Increase score for each suspicious pattern found
                    return true;
                }
            } return false;
        }
    }
}
