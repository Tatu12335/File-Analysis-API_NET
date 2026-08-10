using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Application.Analysis
{
    public class Calculate_Risk_Level
    {
        public Calculate_Risk_Level()
        {


        }

        public async Task<RiskLevel> Calculate(ScanResult scanResult)
        {


            if (scanResult.capabilities == null)
                throw new ArgumentNullException("Scanresult cant be null!");

            return scanResult.capabilities switch
            {
                var c when c.Contains(Capability.ReverseShell)
                || c.Contains(Capability.ProcessInjection)
                => RiskLevel.Critical,

                var c when c.Contains(Capability.PrivilegeEscalation)
                || c.Contains(Capability.Persistance)
                || c.Contains(Capability.CredentialAccess)
                => RiskLevel.High,

                var c when c.Contains(Capability.NetworkCommunication)
                || c.Contains(Capability.Downloader)
                || c.Contains(Capability.FileModification)
                => RiskLevel.Medium,

                _ => RiskLevel.Low,


            };
        }
    }
}
