using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Toolkit_API.Domain.Entities.FileAnalysis
{
    public class ExtractedStrings
    {
        public class ComboRule
        {
            public string Name { get; set; }
            public List<string> RequiredPatternIds { get; set; }
            public double Score { get; set; } = 0;
        }
        public class PatternRule
        {
            public string Id { get; set; }
            public byte[] Name { get; set; }
            public double Score { get; set; }
        }
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
            "vssadmin"

        }.Select(s => Encoding.ASCII.GetBytes(s))
         .ToList();
    }
}
