using System.ComponentModel.DataAnnotations;

namespace Toolkit_API.Application.DTOs.FileDTOs
{
    public class FolderScanDTO
    {
        [Required]
        public string filepath { get; set; }
    }
}
