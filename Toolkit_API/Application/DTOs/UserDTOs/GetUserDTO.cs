using System.ComponentModel.DataAnnotations;

namespace Toolkit_API.Application.DTOs.UserDTOs
{
    public class GetUserDTO
    {
        [Required]
        public string username { get; set; }
    }
}
