using System.Security.Cryptography.X509Certificates;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Policies;
using PeNet;
using System.Text;
using System.Diagnostics;
namespace Toolkit_API.Infrastructure.Services
{
    public class ImportAnalyzer : IImportAnalyzer
    {
        private readonly ICapabilityAnalyzer _capabilityAnalyzer;
        public ImportAnalyzer(ICapabilityAnalyzer capabilityAnalyzer)
        {
           _capabilityAnalyzer = capabilityAnalyzer;
        }
        public IEnumerable<Capability> AnalyzeImports(byte[] fileBytes, ExtractedStrings extractedStrings)
        {
            var capabilities = new List<Capability>();

            var peHeader = new PeFile(fileBytes);
            var importedFunctions = peHeader.ImportedFunctions;

            // Might not be efficient but we'll see
            foreach (var pattern in extractedStrings.Patterns)
            {

                if (importedFunctions.Any(f => f.Name == Encoding.ASCII.GetString(pattern)))
                {
                   
                    var capabilityList = _capabilityAnalyzer.DetectCapabilites(pattern);
                    foreach (var capability in capabilityList)
                    {
                        capabilities.Add(capability);
                        Debug.WriteLine($" Import found [{capability}]");
                    }
                }
            }
            
            return capabilities;
        }
    }
}
