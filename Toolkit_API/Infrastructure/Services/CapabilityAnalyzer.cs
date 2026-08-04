using System.Diagnostics;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Infrastructure.Services
{
    public class CapabilityAnalyzer : ICapabilityAnalyzer
    {
        // this method should only be called from FileAnalysis class.
        public Task<DetectionResult> AnalyzeCapabilities(DetectionResult detectionResult)
        {
            if (detectionResult == null)
                return Task.FromResult(new DetectionResult());

            switch (detectionResult.RuleName)
            {
                case "http://" or "https://":
                    detectionResult.Score = 10;
                    detectionResult.Description = "Application makes unencrypted network calls";
                    detectionResult.RuleName = "Network.Unencrypted";
                    detectionResult.Confidence = 10.0;
                    break;
                case "cmd.exe" or "powershell.exe":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application uses commandline";
                    detectionResult.RuleName = "Uses.CommandLine";
                    detectionResult.Confidence = 20.0;
                    break;
                case string name when name.Contains("CreateRemoteThread")
                       && name.Contains("VirtualAllocEx")
                       && name.Contains("WriteProcessMemory"):
                    detectionResult.Score = 80;
                    detectionResult.Description = "Application injects process/s";
                    detectionResult.RuleName = "Process.Injection";
                    detectionResult.Confidence = 50.0;
                    break;
                default:
                    Debug.WriteLine("Unknown strings");
                    return null;
            }
            return Task.FromResult( detectionResult );

        }

    }
}
