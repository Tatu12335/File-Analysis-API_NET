using System.Diagnostics;
using Toolkit_API.Application.Application_Services.FileOperations;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Application.Analysis
{
    public class StaticScan
    {
        private readonly IFileScanRepo _fileScanRepository;
        private readonly HashOps _hashOps;
        private readonly ICallExternalAPI _callExternalAPI;
        private readonly Calculate_Risk_Level _Risk_Level;
        private readonly ICapabilityAnalyzer _capabilityAnalyzer;
        private readonly ExtractedStrings _extractedStrings;
        private readonly IFileAnalysis _fileAnalysis;
        private readonly IResultRepository _resultRepo;
        private readonly ScoringAlgorithmn _scoringAlgoritmn;
        public StaticScan(IFileScanRepo fileScanRepository,
            HashOps hashOps,
            ICallExternalAPI callExternalAPI,
            Calculate_Risk_Level risk_Level,
            ICapabilityAnalyzer capabilityAnalyzer,
            ExtractedStrings extractedStrings,
            IFileAnalysis fileAnalysis,
            IResultRepository resultRepository,
            ScoringAlgorithmn scoringAlgorithmn)
        {
            _fileScanRepository = fileScanRepository;
            _hashOps = hashOps;
            _callExternalAPI = callExternalAPI;
            _capabilityAnalyzer = capabilityAnalyzer;
            _Risk_Level = risk_Level;
            _extractedStrings = extractedStrings;
            _fileAnalysis = fileAnalysis;
            _resultRepo = resultRepository;
            _scoringAlgoritmn = scoringAlgorithmn;
        }
        public async Task<ScanResult> ScanFile(string filepath, int userId)
        {
            Debug.WriteLine($"Scanning file: {filepath} for user: {userId}");
            if (string.IsNullOrWhiteSpace(filepath))
                return null;


            filepath = Path.GetFullPath(filepath);

            Debug.WriteLine($"Full path of the file: {filepath}");


            var File = await _hashOps.ComputeFileHashAsync(filepath, userId);
            Debug.WriteLine($"File hash: {BitConverter.ToString(File.FileHash).Replace("-", "").ToLower()}");
            /* if (File.Capability != null)
             {


                 var cabalities = await _fileScanRepository.GetCapability(File.FileHash, userId);

                 return new List<ScanResult>() {
                         new ScanResult
                         {
                             capabilities = cabalities,
                             score = File.Score,
                             fileHash = File.FileHash,
                             fileName = File.FileName,
                         }

                 };
             }*/


            var MalwareBazaarResult = await _callExternalAPI.CallAPI(File.FileHash, Environment.GetEnvironmentVariable("Malware_Bazaar_key"));
            var Patterns = await _fileAnalysis.ComboDetection(filepath, _extractedStrings);
            Debug.WriteLine($"Patterns found: {Patterns?.Count() ?? 0}");

            var capabilities = new List<Capability>();

            if (Patterns != null && Patterns.Any())
            {
                foreach (var pattern in Patterns)
                {
                    if (pattern != null)
                    {
                        capabilities.AddRange(Patterns);
                    }
                }
            }

            var uniqueCapabilities = capabilities.Distinct().ToList();

            if(uniqueCapabilities.Any())
                await _fileScanRepository.InsertCapabalities(File.FileHash, File.userId, uniqueCapabilities);
            
            Debug.WriteLine($"Inserted capabilities for file hash: {BitConverter.ToString(File.FileHash).Replace("-", "").ToLower()}");
            //Debug.WriteLine($"Capabilities: {string.Join(", ", capabilities.Select(c => c.ToString()))}");
            return new ScanResult
            {
                capabilities = uniqueCapabilities,

                score = _scoringAlgoritmn
                        .CalculateScore(new ScanResult
                        { capabilities = capabilities, confidence = File.Score, severity = File.Score }),

                fileHash = File.FileHash,
                fileName = File.FileName,
            };
            if (MalwareBazaarResult != null)
            {



                new ScanResult
                {
                    capabilities = capabilities,
                    score = File.Score,
                    fileHash = File.FileHash,
                    fileName = File.FileName,
                    isMalwareBazaarMatch = 1,

                };
            }





            return new ScanResult
            {
                fileHash = File.FileHash,
                fileName = File.FileName,
                score = 0,
            };






        }
    }
}
