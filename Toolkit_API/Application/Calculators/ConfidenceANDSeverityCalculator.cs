using Google.Protobuf.WellKnownTypes;
using System.Diagnostics;
using Toolkit_API.Domain.Entities.FileAnalysis;
using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Application.Calculators
{
    public class ConfidenceANDSeverityCalculator
    {
        
        public IEnumerable<double> CalculateFromResult(IEnumerable<DetectionSource> result)
        {
            if (result == null)
                return new List<double>();
            
            var scoreList = new List<double>();

            foreach(var i in result)
            {
                foreach(var j in i.src)
                {
                    Capability cap = j.Key;
                    Source src = j.Value;

                    var severity = SeverityLookup.GetBaseSeverity(cap);
                    var confidence = ConfidenceLookup.GetBaseConfidence(src);
                    


                    
                    
                    
                }
            }

            return scoreList;
        }
        public double CalculateSeverity(Capability cap)
        {
            var severity = SeverityLookup.GetBaseSeverity(cap);
            return severity;
        }
        public double CalculateConfidence(Source src)
        {
            var confidence = ConfidenceLookup.GetBaseConfidence(src);
            return confidence;
        }
        
    }
}
