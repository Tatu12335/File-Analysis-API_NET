namespace Toolkit_API.Domain.Entities.FileAnalysis
{
    public class CombinedOpcodes
    {
        public List<(byte[] Pattern, int Score)> Patterns { get; set; } = new List<(byte[] Pattern, int Score)>() {
            
            // NOP sled give 10 points
            (new byte[] { 0x90, 0x90, 0x90, 0x90 }, 10),

            // PEB-search give 40
            (new byte[] { 0x64, 0xA1, 0x30, 0x00, 0x00, 0x00 }, 40),
        
            // EICAR test virus signature give 100 points
            (new byte[] { 0x58, 0x35, 0x4F, 0x21, 0x50, 0x41 }, 100)

        };
    }
}
