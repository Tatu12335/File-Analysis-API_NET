using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Application.Analysis
{
    public class CalculateConfidence
    {
        public async Task<ScanResult>  Calculate(IEnumerable<Capability> capabilites)
        {
            if (capabilites.Contains(Capability.NetworkCommunication) && capabilites.Contains(Capability.ServiceInstalation))
            {
                return new ScanResult {
            }
        }
    }
}
