using Toolkit_API.Application.Application_Services.FileOperations;
using Toolkit_API.Application.Interfaces;
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
        public StaticScan(IFileScanRepo fileScanRepository, HashOps hashOps, ICallExternalAPI callExternalAPI, Calculate_Risk_Level risk_Level)
        {
            _fileScanRepository = fileScanRepository;
            _hashOps = hashOps;
            _callExternalAPI = callExternalAPI;
            _Risk_Level = risk_Level;
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
                var risklevel = await _Risk_Level.Calculate()



                return new List<ScanResult>() {
                        new ScanResult
                        {
                            capabilities = cabalities,
                            score = File.Score,
                             
                        }

                };
            }



        }
    }
}
