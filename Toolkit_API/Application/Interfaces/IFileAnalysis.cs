namespace Toolkit_API.Application.Interfaces
{
    public interface IFileAnalysis
    {
        public Task<string> Detect(byte[] bytes);
        public Task DetermineMagicBytes(byte[] bytes);
        public Task<string> AnalyzeFile(string filePath);
    }
}
