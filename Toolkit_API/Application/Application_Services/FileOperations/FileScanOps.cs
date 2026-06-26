using Toolkit_API.Application.Application_Services.FileOperations;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Infrastructure.Services;
namespace Toolkit_API.Application.Application_Services.Operations
{
    public class FileScanOps
    {
        private readonly IFileScanRepo _repository;
        private readonly ICallExternalAPI _externalAPI;
        private readonly HandleResult _handleResult;
        private readonly StaticFileAnalysis _staticFileAnalysis;
        private readonly FileHasher _fileHasher;
        private readonly Toolkit_API.Application.Application_Services.FileOperations.HandleZIP _zipHandler;
        private readonly HandleFolder _handleFolder;
        private readonly IHandleUploadFolder _handleUploadFolder;
        public FileScanOps(IFileScanRepo repository,
            ICallExternalAPI externalAPI,
            HandleResult handleResult,
            StaticFileAnalysis staticFileAnalysis,
            FileHasher fileHasher,
            HandleZIP zipHandler,
            IHandleUploadFolder handleUploadFolder

            )
        {
            _repository = repository;
            _externalAPI = externalAPI;
            _handleResult = handleResult;
            _fileHasher = fileHasher;
            _staticFileAnalysis = staticFileAnalysis;
            _zipHandler = zipHandler;
            _handleUploadFolder = handleUploadFolder;


        }

        public async Task<string> ScanFile(string filePath, int userId)
        {

            if (filePath == null)
                throw new ArgumentNullException();

            filePath = await _handleUploadFolder.SaveFileToUploadFolder(filePath);

            var hash = await _fileHasher.HashFileAsync(filePath);
            // TODO : get the hashes from a cache maybe? And then see if the file is already scanned.
            var hashExists = await _repository.DoubleHash(hash);

            if (hashExists != null)
            {
                var existingFile = await _repository.GetFile(hash, userId);

                if (existingFile != null)
                    return $"{existingFile.Score}";

            }

            var result = await _externalAPI.CallAPI(hash, Environment.GetEnvironmentVariable("Malware_Bazaar_key"));
            var handled = await _handleResult.HandleAsync(result);

            var staticAnalysisResult = await StaticScan(filePath, userId);

            // As i've said before i need to rethink the scoring algorithmn but thats not for now.
            if (handled != null)
                staticAnalysisResult.Score += 30.0;

            await _repository.InsertAll(filePath, userId, staticAnalysisResult.Score);

            return $" {staticAnalysisResult.verdict} ";


        }
        public async Task<FileAnalysisResult> StaticScan(string filePath, int userId)
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
