using Toolkit_API.Application.Interfaces;

namespace Toolkit_API.Application.Application_Services.Operations
{
    public class StaticFileAnalysis
    {
        private readonly IFileAnalysis _fileAnalysis;
        public StaticFileAnalysis(IFileAnalysis fileAnalysis) 
        { 
            _fileAnalysis = fileAnalysis;
        }
        public async Task<string> AnalyzeFile(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException();
            if (!File.Exists(filePath))
                throw new FileNotFoundException();
            var analysisResult = await _fileAnalysis.AnalyzeFile(filePath);
            return analysisResult;
        }
    }
}
