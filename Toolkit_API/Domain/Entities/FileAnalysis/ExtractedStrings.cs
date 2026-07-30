using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolkit_API.Domain.Entities.FileAnalysis
{
    public class ExtractedStrings
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
            "QueueUserAPC"

        }.Select(s => Encoding.ASCII.GetBytes(s))
         .ToList();
    }
}
