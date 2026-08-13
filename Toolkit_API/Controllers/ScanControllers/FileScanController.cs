using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Diagnostics;
using Toolkit_API.Application.Analysis;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.DTOs.FIleDTOs;
using Toolkit_API.Middleware;

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
        private readonly IResultRepository _resultRepository;
        private readonly IScan _ScannerService;
        public FileScanController(IhangfireService hangfireService, StaticScan scan, IResultRepository resultRepository, IScan scannerService)
        {
            _HangfireService = hangfireService;
            _scan = scan;
            _resultRepository = resultRepository;
            _ScannerService = scannerService;
        }
        
        [HttpPost("Scan")]
        public async Task<IActionResult> ScanFile(FileScanDTO fileScanDTO)
        {

            _HangfireService.storage(Environment.GetEnvironmentVariable("HANGFIRE"));

            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var jobId = BackgroundJob.Enqueue<IScan>(x => x.ScanFile(fileScanDTO.filePath, fileScanDTO.userId, null!));

            return Ok(new { jobId = jobId });

        }
        [HttpGet("Scan/Capabilities/{jobId}")]
        public async Task<IActionResult> GetCapabilities(string jobId)
        {
            var capabilities = await _resultRepository.GetCapabilities(jobId);
            return Ok(new {Capabilities = capabilities });
        }
        [HttpGet("Scan/Fetch/{jobId}")]
        public async Task<IActionResult> GetScanResult(string jobId)
        {

            var result = await _resultRepository.GetResultAsync(jobId);
           
           /* var result2 = result.capabilities
                .Select(x => x.ToString())
                .ToList();*/

            
            return Ok(new { RawScanResult = result});
        }


    }
}
