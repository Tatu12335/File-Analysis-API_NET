using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Application.Interfaces
{
    public interface IImportAnalyzer
    {
        public IEnumerable<Capability> AnalyzeImports(byte[] fileBytes, ExtractedStrings extractedStrings);
    }
}
