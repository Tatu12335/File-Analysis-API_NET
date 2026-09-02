using MailKit;
using Toolkit_API.Domain.Entities.FileAnalysis;

namespace Toolkit_API.Application.Calculators
{
    public class ConfidenceLookup
    {
        private static readonly Dictionary<Source, double> BaseConfidence = new()
        {
            [Source.Import] = 0.9,
            [Source.String] = 0.5
        };

        public static double GetBaseConfidence(Source src) =>
            BaseConfidence.GetValueOrDefault(src, 0.3);
    }
}
