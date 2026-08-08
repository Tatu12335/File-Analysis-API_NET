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
using Microsoft.AspNetCore.SignalR;
using Hangfire.Server;
using Toolkit_API.Domain.Entities.Files;
using Toolkit_API.Infrastructure.Services;

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
        public FileScanController( IhangfireService hangfireService, StaticScan scan, IResultRepository resultRepository, IScan scannerService)
        { 
            _HangfireService = hangfireService;
            _scan = scan;
            _resultRepository = resultRepository;
            _ScannerService = scannerService;
        }
        // todo : enqueue the scan file method to hangfire and return the job id to the user
        // Then make a endpoint that will return the scan result based on the job id and user id
        [HttpPost("Scan")]
        public async Task<IActionResult> ScanFile(FileScanDTO fileScanDTO)
        {
            
            _HangfireService.storage(Environment.GetEnvironmentVariable("HANGFIRE"));
            
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var jobId = BackgroundJob.Enqueue<IScan>(x => x.ScanFile(fileScanDTO.filePath, fileScanDTO.userId, null!));

            return Ok(new {jobId = jobId} );

        }
        [HttpGet("Scan/Fetch/{jobId}")]
        public async Task<IActionResult> GetScanResult(string jobId)
        {
            
            var result = await _resultRepository.GetResultAsync(jobId);
            return Ok(new { result = result });
        }


    }
}
