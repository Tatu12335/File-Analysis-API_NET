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
       /* public Task<ScanResult> AnalyzeCapabilities(DetectionResult detectionResult)
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

        }*/
        // I know this is not the most efficient way to do this, but it works for now. We can optimize this later if needed.
        public ScanResult? GetCapabilitiesName(ReadOnlySpan<byte> pattern) 
        {
            var capabilities = new HashSet<Capability>();

            if(pattern.IndexOf("http://"u8) >= 0)
            {
                capabilities.Add(Capability.NetworkCommunication);
            }
            if(pattern.IndexOf("https://"u8) >= 0 || pattern.IndexOf("InternetOpenA"u8) >= 0
                || pattern.IndexOf("InternetOpenW"u8) >= 0 ) 
            {
            
                capabilities.Add(Capability.NetworkCommunication);
            }
            if(pattern.IndexOf("cmd.exe"u8) >= 0 || pattern.IndexOf("powershell.exe"u8) >= 0)
            {
                capabilities.Add(Capability.CommandLineExecution);
            }
            if(pattern.IndexOf("CreateRemoteThread"u8) >= 0 || pattern.IndexOf("VirtualAlloc"u8) >= 0
                || pattern.IndexOf("WriteProcessMemory"u8) >= 0)
            {
                capabilities.Add(Capability.ProcessInjection);
            }
            if(pattern.IndexOf("CreateProcessA"u8) >= 0 || pattern.IndexOf("CreateProcessW"u8) >= 0)
            {
                capabilities.Add(Capability.ProcessManipulation);
            }
            if(pattern.IndexOf("GetAsyncKeyState"u8) >= 0 || pattern.IndexOf("GetKeyState"u8) >= 0)
            {
                capabilities.Add(Capability.Keylogging);
            }
            if(pattern.IndexOf("CreateFileA"u8) >= 0 || pattern.IndexOf("CreateFileW"u8) >= 0)
            {
                capabilities.Add(Capability.FileManipulation);
            }
            if(pattern.IndexOf("DeleteFileA"u8) >= 0 || pattern.IndexOf("DeleteFileW"u8) >= 0)
            {
                capabilities.Add(Capability.FileDeletion);
            }
            if(pattern.IndexOf("OpenProcess"u8) >= 0)
            {
                capabilities.Add(Capability.ProcessEnumeration);
            }
            if(pattern.IndexOf("ReadProcessMemory"u8) >= 0)
            {
                capabilities.Add(Capability.MemoryReading);
            }
            if(pattern.IndexOf("IsDebuggerPresent"u8) >= 0)
            {
                capabilities.Add(Capability.AntiDebug);
            }
            if(pattern.IndexOf("IsVirtualMachine"u8) >= 0)
            {
                capabilities.Add(Capability.AntiVM);
            }
            if(pattern.IndexOf("CreateServiceA"u8) >= 0 || pattern.IndexOf("CreateServiceW"u8) >= 0)
            {
                capabilities.Add(Capability.ServiceInstalation);
            }
            if(pattern.IndexOf("RegCreateKeyA"u8) >= 0 || pattern.IndexOf("RegCreateKeyW"u8) >= 0
                || pattern.IndexOf("RegSetValueA"u8) >= 0 || pattern.IndexOf("RegSetValueW"u8) >= 0
                || pattern.IndexOf("RegDeleteKeyA"u8) >= 0 || pattern.IndexOf("RegDeleteKeyW"u8) >= 0
                || pattern.IndexOf("RegDeleteValueA"u8) >= 0 || pattern.IndexOf("RegDeleteValueW"u8) >= 0)
            {
                capabilities.Add(Capability.RegisteryModification);
            }
            if(pattern.IndexOf("GetProcAddress"u8) >= 0)
            {
                capabilities.Add(Capability.ProcessEnumeration);
            }
            
            Debug.WriteLine($"Capabilities found: {string.Join(", ", capabilities)}");

            return new ScanResult() { capabilities = capabilities };
        }
    }
        
}
