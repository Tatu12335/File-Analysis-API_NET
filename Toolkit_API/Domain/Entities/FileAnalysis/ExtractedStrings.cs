using System.Text;

namespace Toolkit_API.Domain.Entities.FileAnalysis
{
    public class ExtractedStrings
    {

        public List<byte[]> Patterns = new List<string>()
        {
            // network calls
            "http://",
            "https://",
            "ftp://",
            // Windows tools
            "cmd.exe",
            "powershell.exe",
            // Proccess injection
            "CreateRemoteThread",
            "VirtualAllocEx",
            "WriteProcessMemory",
            "GetProcAddress",
            "VirtualAlloc",
            "VirtualProtect",
            "NtMapViewOfSection",
            "ZwMapViewOfSection",
            "QueueUserAPC",
            "RtlMoveMemory",
            "memcpy",
            "SetThreadContext",
            "GetThreadContext",
            // KeyLogging
            "GetAsyncKeyState",
            // Anti-Analysis
            "IsDebuggerPresent",
            "CheckRemoteDebuggerPresent",
            "NtQueryInformationProcess",
            "VirtualProtectEx",
            "OutputDebugStringA",
            // Persistance
            "RegSetValueExA",
            "RegSetValueExW",
            "CreateServiceA",
            "CreateServiceW",
            "schtasks.exe",
            // Ransomware
            "bcdedit.exe",
            "vssadmin",
            "CryptEncrypt",
            "CryptDecrypt",
            "CryptGenKey",
            // Living off the land
            "rundll32.exe",
            "regsvr32",
            "certutil.exe",
            "mshta.exe",
            "wmic.exe",
            // C2
            "InternetOpenA",
            "InternetOpenW",
            "InternetConnectA",
            "internetConnectUrlW",
            "HttpSendRequestA",
            "HttpSendRequestW",
            "URLDownloadToFileA",
            "URLDownloadToFileW",



        }.Select(s => Encoding.ASCII.GetBytes(s))
         .ToList();
    }
}
