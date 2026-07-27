using System.Diagnostics;
using Toolkit_API.Application.Application_Services.FileOperations;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.Infrastructure.Services;
using Toolkit_API.Domain.Policies;
namespace Toolkit_API.Application.Application_Services.Operations
{
    public class FileScanOps
    {
        private readonly IFileScanRepo _repository;
        private readonly ICallExternalAPI _externalAPI;
        private readonly HandleResult _handleResult;
        private readonly StaticFileAnalysis _staticFileAnalysis;
        private readonly Toolkit_API.Application.Application_Services.FileOperations.HandleZIP _zipHandler;
        private readonly HandleFolder _handleFolder;
        private readonly IHandleUploadFolder _handleUploadFolder;
        private readonly List<ExtractedStrings.ComboRule> _comboRules;
        private readonly List<ExtractedStrings.PatternRule> _patternRules;
        public FileScanOps
            (
                IFileScanRepo repository,
                ICallExternalAPI externalAPI,
                HandleResult handleResult,
                StaticFileAnalysis staticFileAnalysis,               
                HandleZIP zipHandler,
                IHandleUploadFolder handleUploadFolder,
                List<ExtractedStrings.ComboRule> comboRules,
                List<ExtractedStrings.PatternRule> patternRules
            )
        {
            _repository = repository;
            _externalAPI = externalAPI;
            _handleResult = handleResult;
            _staticFileAnalysis = staticFileAnalysis;
            _zipHandler = zipHandler;
            _handleUploadFolder = handleUploadFolder;
            _comboRules = comboRules;
            _patternRules = patternRules;

        }
        // I also need to rethink this whole blocks efficiency alot '-'
        public async Task<List<ScanResult>> ScanFile(string filePath, int userId)
        {

            if (filePath == null)
                throw new ArgumentNullException();

            filePath = filePath.Trim('"');
            filePath = await _handleUploadFolder.SaveFileToUploadFolder(filePath);

            Debug.WriteLine($"File saved to upload folder: {filePath}");


            
            
            var hashExists = await _repository.DoubleHash(hash);

            if (hashExists != null)
            {
                var existingFile = await _repository.GetFile(hash, userId);

                if (existingFile != null)
                    return new List<ScanResult>
                    {
                        new ScanResult
                        {
                            score = existingFile.Score,
                            riskLevel = existingFile.Score > 50.0 ? RiskLevel.High : RiskLevel.Low,
                            capabilities = new List<Capability>
                            {
                                Capability.None
                            },
                        }
                    };

            }


            var result = await _externalAPI.CallAPI(hash, Environment.GetEnvironmentVariable("Malware_Bazaar_key"));
            var handled = await _handleResult.HandleAsync(result);

            var staticAnalysisResult = await StaticScan(filePath, userId);

            // As i've said before i need to rethink the scoring algorithmn but thats not for now.
            if (handled != null)
                staticAnalysisResult.Score += 100.0;

            await _repository.InsertAll(filePath, userId, staticAnalysisResult.Score);

            return new List<ScanResult>
            {
                new ScanResult
                {
                    score = staticAnalysisResult.Score,
                    riskLevel = staticAnalysisResult.Score > 50.0 ? RiskLevel.High : RiskLevel.Low,
                    capabilities = new List<Capability>
                    {
                        Capability.None
                    },
                }
            };


        }
        public async Task<DetectionResult> StaticScan(string filePath, int userId)
        {
            if (filePath == null)
                throw new ArgumentNullException();
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            var analysisResult = await _staticFileAnalysis.AnalyzeFile(filePath);
            return analysisResult;

        }

    }

}
