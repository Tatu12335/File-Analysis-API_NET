using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using Toolkit_API.Application.Application_Services.FileOperations;
using Toolkit_API.Application.Application_Services.Operations;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.DTOs.FileDTOs;
using Toolkit_API.DTOs.FIleDTOs;
using Hangfire;
using Toolkit_API.Application.Analysis;

namespace Toolkit_API.Controllers.ScanControllers
{
    [EnableRateLimiting("Fixed")]
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class FileScanController : ControllerBase
    {
        
       
        private readonly IhangfireService _HangfireService;
        private readonly StaticScan _scan;
        public FileScanController( IhangfireService hangfireService, StaticScan scan)
        {
            
            
            _HangfireService = hangfireService;
            _scan = scan;
        }
        [HttpPost("Scan/File")]
        public async Task<IActionResult> ScanFile([FromBody] FileScanDTO scanDTO)
        {
            _HangfireService.storage(Environment.GetEnvironmentVariable("HANGFIRE"));
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            BackgroundJob.Enqueue(() => _scan.ScanFile(scanDTO.filePath,scanDTO.userId));

            return Ok(BackgroundJob.Enqueue(() => _scan.ScanFile(scanDTO.filePath, scanDTO.userId)));

        }
      

    }
}
