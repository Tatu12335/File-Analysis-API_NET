
using System.Text;

namespace Toolkit_API.Domain.Entities.Files
{
 
    public class Imports
    {
        public List<byte[]> Patterns = new List<string>()
        {
            "http://",
            "https://",
            "ftp://",
            "cmd.exe",
            "powershell.exe",
            "User-Agent",
            "CreateRemoteThread",
            "VirtualAllocEx",
            "WriteProcessMemory",
            "GetProcAddress",
            "GetAsyncKeyState",
            "vssadmin",
            "VirtualAlloc",
            "VirtualProtect",
            "NtMapViewOfSection",
            "ZwMapViewOfSection",
            "QueueUserAPC",
            ""

        }.Select(s => Encoding.ASCII.GetBytes(s))
         .ToList();
    }
}
