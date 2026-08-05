using System.Diagnostics;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Infrastructure.Services
{
    public class CapabilityAnalyzer : ICapabilityAnalyzer
    {
        // this method should only be called from FileAnalysis class.
        public Task<ScanResult> AnalyzeCapabilities(DetectionResult detectionResult)
        {
            if (detectionResult == null)
                return Task.FromResult(new ScanResult());
            List<Capability> capabilities = new List<Capability>();
            switch (detectionResult.RuleName)
            {
                case "http://":
                    detectionResult.Score = 10;
                    detectionResult.Description = "Application makes unencrypted network calls";
                    detectionResult.RuleName = "Network.Unencrypted";
                    detectionResult.Confidence = 10.0;
                    capabilities.Add(Capability.NetworkCommunication);
                    break;
                case "https://":
                    detectionResult.Score = 10;
                    detectionResult.Description = "Application makes encrypted network calls";
                    detectionResult.RuleName = "Network.Encrypted";
                    detectionResult.Confidence = 10.0;
                    capabilities.Add(Capability.NetworkCommunication);
                    break;

                case "cmd.exe" or "powershell.exe":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application uses commandline";
                    detectionResult.RuleName = "Uses.CommandLine";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.CommandLineExecution);
                    break;
                case "CreateRemoteThread" or "VirtualAllocEx" or "WriteProcessMemory":
                    detectionResult.Score = 80;
                    detectionResult.Description = "Application injects process/s";
                    detectionResult.RuleName = "Process.Injection";
                    detectionResult.Confidence = 50.0;
                    capabilities.Add(Capability.ProcessInjection);
                    break;
                case "InternetOpenA" or "InternetOpenW":
                    detectionResult.Score = 10;
                    detectionResult.Description = "Application makes network calls";
                    detectionResult.RuleName = "Network.Communication";
                    detectionResult.Confidence = 10.0;
                    capabilities.Add(Capability.NetworkCommunication);
                    break;
                case "CreateProcessA" or "CreateProcessW":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application creates new process/s";
                    detectionResult.RuleName = "Process.Creation";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.CommandExecution);
                    break;


                default:
                    Debug.WriteLine("Unknown strings");
                    break;
            }
            return Task.FromResult(new ScanResult() { capabilities = capabilities });

        }

    }
}
