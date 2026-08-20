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
        public async Task<IEnumerable<DetectionSource>> CreateContext(string filepath, ExtractedStrings extractedStrings)
        {
            var imports = await _fileAnalysis.ImportAnalysis(filepath, extractedStrings);
            var ComboFindings = await _fileAnalysis.ComboDetection(filepath, extractedStrings);
            var PlainFindings = new List<DetectionSource>();



            if (!ComboFindings.Any() && !imports.Any())
                return new List<DetectionSource>();

                foreach (var import in imports)
                {
                    PlainFindings.Add(RegisterFindings(import, Source.Import));
                }


            foreach (var str in ComboFindings)
            {
                PlainFindings.Add(RegisterFindings(str, Source.String));
            }

            return PlainFindings;
        }
        public DetectionSource RegisterFindings(Capability capability, Source source)
        {
           
            var CapabalityList = new Dictionary<Capability, Source>();

            CapabalityList[capability] = CapabalityList.TryGetValue(capability, out var existing)
                ? existing | source
                : source;
             
            
            
            return new DetectionSource 
            { src = CapabalityList };
        }
    }
}
