using Microsoft.AspNetCore.Mvc;
using Toolkit_API.DTOs;

namespace Toolkit_API.Controllers
{
    [ApiController]
    [Route("/")]
    public class UserController : ControllerBase
    {

        [HttpPost("Create_User")]
        public async Task<IActionResult> CreateUser([FromHeader] CreateUserDTO userDTO)
        {

        }
    }
}
