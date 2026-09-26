using System.ComponentModel.DataAnnotations;

namespace Toolkit_API.Application.DTOs.FileDTOs
{
    public class FileAnalysisDTO
    {
        [Required]
        public string FilePath { get; set; }
    }
}
