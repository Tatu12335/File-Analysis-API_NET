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
        public double CalculateOverallConfidence(IEnumerable<DetectionSource> src)
        {
            if (src == null || !src.Any())
                return 0.0;
            var confidences = src.Select(f => f.src.Values.Select(cap => ConfidenceLookup.GetBaseConfidence(cap))).SelectMany(s => s).ToList();
            
            var max = confidences.First();
            
            var additionalConfidences = confidences.Skip(1)
                .Select((c, index) => c * Math.Pow(0.5, index + 1))
                .Sum();

            return Math.Min(max + additionalConfidences, 1.0); // Cap the confidence at 1
        }
        public double CalculateOverallSeverity(IEnumerable<DetectionSource> src)
        {
            if (src == null || !src.Any())
                return 0.0;
            
            var severities = src.Select(f => f.src.Keys.Select(cap => SeverityLookup.GetBaseSeverity(cap))).SelectMany(s => s).ToList();

            var max = severities.First();

            var additionalSeverities = severities.Skip(1)
                .Select((s,index) => s * Math.Pow(0.5, index + 1))
                .Sum();

            return Math.Min(max + additionalSeverities, 10.0); // Cap the severity at 10

        }
        
    }
}
