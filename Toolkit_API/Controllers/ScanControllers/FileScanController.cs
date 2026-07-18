using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using Toolkit_API.Application.Application_Services.FileOperations;
using Toolkit_API.Application.Application_Services.Operations;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.DTOs.FileDTOs;
using Toolkit_API.DTOs.FIleDTOs;
using Hangfire;

namespace Toolkit_API.Controllers.ScanControllers
{
    [EnableRateLimiting("Fixed")]
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class FileScanController : ControllerBase
    {
        private readonly FileScanOps _fileScanOps;
        private readonly HandleFolder _Handler;
        private readonly IhangfireService _HangfireService;
        public FileScanController(FileScanOps fileScanOps, HandleFolder handleFolder, IhangfireService hangfireService)
        {
            _fileScanOps = fileScanOps;
            _Handler = handleFolder;
            _HangfireService = hangfireService;
        }
        [HttpPost("Scan/File")]
        public async Task<IActionResult> ScanFile([FromBody] FileScanDTO scanDTO)
        {
            _HangfireService.storage(Environment.GetEnvironmentVariable("HANGFIRE"));
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var res = BackgroundJob.Enqueue(() => _fileScanOps.ScanFile(scanDTO.filePath, 2025));

            return Ok(res);

        }
        [HttpPost("Scan/Folder")]
        public async Task<IActionResult> ScanFolder([FromBody] FolderScanDTO scanDTO)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _Handler.Handler(scanDTO.filepath, userId);
            return Ok(result);
        }

    }
}
