namespace Toolkit_API.Domain.Policies
{
    public static class CapabilityRule
    {
        public static CapabilityRuleset[] rules = [
            new CapabilityRuleset(
                Capability.NetworkCommunication,
                ["http://", "https:/", "InternetOpenA", "InternetOpenW"]),

            new CapabilityRuleset(Capability.ProcessInjection,
                ["CreateRemoteThread", "VirtualAllocEx", "WriteProcessMemory"]),

            new CapabilityRuleset(Capability.CommandLineExecution,
                ["cmd.exe","powershell.exe"]),

            new CapabilityRuleset(Capability.AntiDebug,
                ["IsDebuggerPresent","CheckRemoteDebuggerPresent","NtQueryInformationProcess","VirtualProtectEx","OutputDebugStringA"]),

            new CapabilityRuleset(Capability.Keylogging,
                ["GetAsyncKeyState"]),

            new CapabilityRuleset(Capability.RegisteryModification,
                ["RegSetValueExA","RegSetValueExW"]),

            new CapabilityRuleset(Capability.Persistance,
                ["CreateServiceA","CreateServiceW","schtasks.exe"]),

            new CapabilityRuleset(Capability.FileEncryption,
                ["CryptEncrypt","CryptDecrypt"]),



            ];
    }
}
