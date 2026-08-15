using k8s.Models;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.Domain.Policies;
namespace Toolkit_API.Infrastructure.Services
{
    public class DetectionSourceBuilder : IDetectionSourceBuilder
    {
        private readonly IFileAnalysis _fileAnalysis;
        public DetectionSourceBuilder(IFileAnalysis fileAnalysis)
        {
            _fileAnalysis = fileAnalysis;
        }
        public async Task CreateContext(string filepath, ExtractedStrings extractedStrings)
        {
            var imports = await _fileAnalysis.ImportAnalysis(filepath, extractedStrings);
            var strings = await _fileAnalysis.ComboDetection(filepath, extractedStrings);
            var capabilityDict = new Dictionary<Capability, Source>();

            if (!strings.Any() && !imports.Any())
                return;
            // WHAT THE FUCK IS THIS SHIT!
            foreach(var import in imports)
            {
                await RegisterFindings(import, Source.Import);
                if(strings.Any()) 
                {
                    foreach(var str in strings)
                    {
                        await RegisterFindings(str, Source.String);
                    }
                }
            }
            foreach(var str in strings)
            {
                await RegisterFindings(str, Source.String); 
            }

            return;
        }
        public async Task<DetectionSource> RegisterFindings(Capability capability, Source source)
        {
           
            var CapabalityList = new Dictionary<Capability, Source>();


             
            
            
            return new DetectionSource 
            { result = CapabalityList };
        }
    }
}
