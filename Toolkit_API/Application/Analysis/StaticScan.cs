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
        public StaticScan(IFileScanRepo fileScanRepository, 
            HashOps hashOps, 
            ICallExternalAPI callExternalAPI, 
            Calculate_Risk_Level risk_Level, 
            ICapabilityAnalyzer capabilityAnalyzer,
            ExtractedStrings extractedStrings, 
            IFileAnalysis fileAnalysis)
        {
            _fileScanRepository = fileScanRepository;
            _hashOps = hashOps;
            _callExternalAPI = callExternalAPI;
            _capabilityAnalyzer = capabilityAnalyzer;
            _Risk_Level = risk_Level;
            _extractedStrings = extractedStrings;
            _fileAnalysis = fileAnalysis;
        }
        public async Task<List<ScanResult>> ScanFile(string filepath, int userId)
        {
            if (string.IsNullOrWhiteSpace(filepath))
                return null;
                

            filepath = Path.GetFullPath(filepath);

            var File = await _hashOps.ComputeFileHashAsync(filepath, userId);

            if (File != null)
            {
                var cabalities = await _fileScanRepository.GetCapability(File.FileHash, userId);
                    
                return new List<ScanResult>() {
                        new ScanResult
                        {
                            capabilities = cabalities,
                            score = File.Score,
                            riskLevel = 0,
                            fileHash = File.FileHash,
                            fileName = File.FileName,
                        }

                };
            }

            var MalwareBazaarResult = await _callExternalAPI.CallAPI(File.FileHash, Environment.GetEnvironmentVariable("Malware_Bazaar_key"));

            if(MalwareBazaarResult != null)
            {
                
                
                return new List<ScanResult>() {
                        new ScanResult
                        {
                            capabilities = cabalities,
                            score = File.Score,
                            riskLevel = riskLevel,
                            fileHash = File.FileHash,
                            fileName = File.FileName,
                        }
                };
            }


            return new List<ScanResult>() { 
                new ScanResult
                {
                    fileHash = File.FileHash,
                    fileName = File.FileName,
                    score = 0,
                }
            };
            
            



        }
    }
}
