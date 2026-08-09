using Org.BouncyCastle.Utilities;
using System.Diagnostics;
using System.Text;
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
                case "GetAsyncKeyState" or "GetKeyState":
                    detectionResult.Score = 80;
                    detectionResult.Description = "Application logs keystrokes";
                    detectionResult.RuleName = "Keylogging";
                    detectionResult.Confidence = 50.0;
                    capabilities.Add(Capability.Keylogging);
                    break;
                case "CreateFileA" or "CreateFileW":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application creates files";
                    detectionResult.RuleName = "File.Creation";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.FileModification);
                    break;
                case "DeleteFileA" or "DeleteFileW":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application deletes files";
                    detectionResult.RuleName = "File.Deletion";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.FileDeletion);
                    break;
                case "OpenProcess":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application opens processes";
                    detectionResult.RuleName = "Process.Opening";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.ProcessEnumeration);
                    break;
                case "ReadProcessMemory":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application reads process memory";
                    detectionResult.RuleName = "Process.MemoryReading";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.MemoryReading);
                    break;
                case "IsDebuggerPresent":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application checks for debugger";
                    detectionResult.RuleName = "AntiDebug";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.AntiDebug);
                    break;
                case "IsVirtualMachine":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application checks for virtual machine";
                    detectionResult.RuleName = "AntiVM";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.AntiVM);
                    break;
                case "CreateServiceA" or "CreateServiceW":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application creates service";
                    detectionResult.RuleName = "Service.Creation";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.ServiceInstalation);
                    break;
                case "RegCreateKeyA" or "RegCreateKeyW":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application creates registry key";
                    detectionResult.RuleName = "Registry.Creation";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.RegisteryModification);
                    break;
                case "RegSetValueA" or "RegSetValueW":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application sets registry value";
                    detectionResult.RuleName = "Registry.SetValue";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.RegisteryModification);
                    break;
                case "RegDeleteKeyA" or "RegDeleteKeyW":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application deletes registry key";
                    detectionResult.RuleName = "Registry.Deletion";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.RegisteryModification);
                    break;
                case "RegDeleteValueA" or "RegDeleteValueW":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application deletes registry value";
                    detectionResult.RuleName = "Registry.DeleteValue";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.RegisteryModification);
                    break;
                case "GetProcAddress":
                    detectionResult.Score = 20;
                    detectionResult.Description = "Application gets process address";
                    detectionResult.RuleName = "Process.GetAddress";
                    detectionResult.Confidence = 20.0;
                    capabilities.Add(Capability.ProcessEnumeration);
                    break;

                default:
                    Debug.WriteLine($"Unknown rule name: {detectionResult.RuleName}");
                    Debug.WriteLine("Unknown strings");
                    break;
            }

            return Task.FromResult(new ScanResult() { capabilities = capabilities });

        }
        // I know this is not the most efficient way to do this, but it works for now. We can optimize this later if needed.
        public ScanResult? GetCapabilitiesName(ReadOnlySpan<byte> pattern) => pattern switch
        {
            

            var p when p.IndexOf(Encoding.ASCII.GetBytes("http://")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.NetworkCommunication }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("https://")) >= 0 =>
            new ScanResult { capabilities = new List<Capability> { Capability.NetworkCommunication }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("cmd.exe")) >= 0 =>
            new ScanResult { capabilities = new List<Capability> { Capability.CommandExecution } },

            var p when p.IndexOf(Encoding.ASCII.GetBytes("powershell.exe")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.CommandExecution }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("CreateRemoteThread")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.ProcessManipulation }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("VirtualAllocEx")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.ProcessManipulation }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("WriteProcessMemory")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.ProcessManipulation }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("InternetOpenA")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.NetworkCommunication }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("InternetOpenW")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.NetworkCommunication }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("CreateProcessA")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.ProcessManipulation }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("CreateProcessW")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.ProcessManipulation }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("GetAsyncKeyState")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.Keylogging}},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("GetKeyState")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.Keylogging }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("CreateFileA")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.FileManipulation }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("CreateFileW")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.FileManipulation }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("DeleteFileA")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.FileManipulation }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("DeleteFileW")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.FileManipulation }},
            var p when p.IndexOf(Encoding.ASCII.GetBytes("OpenProcess")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.ProcessManipulation }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("ReadProcessMemory")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.ProcessManipulation }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("IsDebuggerPresent")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.AntiDebug }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("IsVirtualMachine")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.AntiVM }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("CreateServiceA")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.ServiceInstalation }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("CreateServiceW")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.ServiceInstalation }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("RegCreateKeyA")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.RegisteryModification }},

            var p when p.IndexOf(Encoding.ASCII.GetBytes("RegCreateKeyW")) >= 0 =>
            new ScanResult {capabilities = new List<Capability> { Capability.RegisteryModification }},

            _ => null
        };
    }
        
}
