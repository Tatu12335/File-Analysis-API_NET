using System.Diagnostics;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Infrastructure.Services
{
    public class CapabilityAnalyzer : ICapabilityAnalyzer
    {
        // this method should only be called from FileAnalysis class.
        
        // I know this is not the most efficient way to do this, but it works for now. We can optimize this later if needed.
        public IEnumerable<Capability> GetCapabilitiesName(ReadOnlySpan<byte> pattern)
        {
            var capabilities = new HashSet<Capability>();

            if (pattern.IndexOf("http://"u8) >= 0)
            {
                capabilities.Add(Capability.NetworkCommunication);
            }
            if (pattern.IndexOf("https://"u8) >= 0 || pattern.IndexOf("InternetOpenA"u8) >= 0
                || pattern.IndexOf("InternetOpenW"u8) >= 0)
            {

                capabilities.Add(Capability.NetworkCommunication);
            }
            if (pattern.IndexOf("cmd.exe"u8) >= 0 || pattern.IndexOf("powershell.exe"u8) >= 0)
            {
                capabilities.Add(Capability.CommandLineExecution);
            }
            if (pattern.IndexOf("CreateRemoteThread"u8) >= 0 || pattern.IndexOf("VirtualAlloc"u8) >= 0
                || pattern.IndexOf("WriteProcessMemory"u8) >= 0)
            {
                capabilities.Add(Capability.ProcessInjection);
            }
            if (pattern.IndexOf("CreateProcessA"u8) >= 0 || pattern.IndexOf("CreateProcessW"u8) >= 0)
            {
                capabilities.Add(Capability.ProcessManipulation);
            }
            if (pattern.IndexOf("GetAsyncKeyState"u8) >= 0 || pattern.IndexOf("GetKeyState"u8) >= 0)
            {
                capabilities.Add(Capability.Keylogging);
            }
            if (pattern.IndexOf("CreateFileA"u8) >= 0 || pattern.IndexOf("CreateFileW"u8) >= 0)
            {
                capabilities.Add(Capability.FileManipulation);
            }
            if (pattern.IndexOf("DeleteFileA"u8) >= 0 || pattern.IndexOf("DeleteFileW"u8) >= 0)
            {
                capabilities.Add(Capability.FileDeletion);
            }
            if (pattern.IndexOf("OpenProcess"u8) >= 0)
            {
                capabilities.Add(Capability.ProcessEnumeration);
            }
            if (pattern.IndexOf("ReadProcessMemory"u8) >= 0)
            {
                capabilities.Add(Capability.MemoryReading);
            }
            if (pattern.IndexOf("IsDebuggerPresent"u8) >= 0)
            {
                capabilities.Add(Capability.AntiDebug);
            }
            if (pattern.IndexOf("IsVirtualMachine"u8) >= 0)
            {
                capabilities.Add(Capability.AntiVM);
            }
            if (pattern.IndexOf("CreateServiceA"u8) >= 0 || pattern.IndexOf("CreateServiceW"u8) >= 0)
            {
                capabilities.Add(Capability.ServiceInstalation);
            }
            if (pattern.IndexOf("RegCreateKeyA"u8) >= 0 || pattern.IndexOf("RegCreateKeyW"u8) >= 0
                || pattern.IndexOf("RegSetValueA"u8) >= 0 || pattern.IndexOf("RegSetValueW"u8) >= 0
                || pattern.IndexOf("RegDeleteKeyA"u8) >= 0 || pattern.IndexOf("RegDeleteKeyW"u8) >= 0
                || pattern.IndexOf("RegDeleteValueA"u8) >= 0 || pattern.IndexOf("RegDeleteValueW"u8) >= 0)
            {
                capabilities.Add(Capability.RegisteryModification);
            }
            if (pattern.IndexOf("GetProcAddress"u8) >= 0)
            {
                capabilities.Add(Capability.ProcessEnumeration);
            }

            Debug.WriteLine($"Capabilities found: {string.Join(", ", capabilities)}");

            return capabilities = capabilities;
        }
    }

}
