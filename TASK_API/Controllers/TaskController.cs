using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TASK_API.Domain;
using Toolkit_API.Application.Application_Services.FileOperations;
using Toolkit_API.Application.Application_Services.Operations;
using Toolkit_API.Infrastructure.Services;

namespace TASK_API.Controllers
{
    // NOTE : I will not make professional architecture,
    // just because of the time constraint and the fact that this is a test task.
    // So I will just put everything in the controller and make it work
    // Im probably gonna do 3 layer architecture in the future, because i think project this small doesn't need onion/clean architecture
    public class TaskController : Controller
    {
        private readonly FileScanOps _scanService;

        private readonly HandleFolder _handleFolder;
        

        // this whole controller is all over the fucking place :(
        public TaskController(FileScanOps scanService, HandleFolder handleFolder)
        {
            _scanService = scanService;
            _handleFolder = handleFolder;
       
        }
        // Also Note that for filescanning you should use this endpoint because this has the [ wip ] background workers 
        // Also i know i could have use hangfire but i decided to try do the background worker myself!
        [HttpPost("scan")]
        public async Task<IActionResult> Scan([FromBody] ScanDTO scan)
        {
            try
            {
                var result = string.Empty;
                var jobs = await GetPendingJobs();

                if (jobs == null || !jobs.Any())
                {
                    result = await _scanService.ScanFile(scan.FilePath, scan.UserId);
                    return Ok(result);
                }

                foreach (var job in jobs)
                {
                    // result = await _scanService.ScanFile(, scan.UserId);
                    var jobId = await GetJobId(job.FilePath);

                    

                    
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-folder")]
        public async Task<IActionResult> AddFolder(int userId, string folderPath)
        {
            try
            {
                await GetPendingJobs();
                var files = await _handleFolder.Handler(folderPath, userId);

                foreach (var file in files.Files)
                {
                    await AddJob(userId, file, 0);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pending-jobs")]
        public async Task<IEnumerable<Job>> GetPendingJobs()
        {
            try
            {
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION2");
                using (var conn = new SqlConnection(connectionString))
                {
                    var query = "SELECT Filepath FROM job WHERE Jobstatus = '0'";
                    var result = await conn.QueryAsync<Job>(query);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        [HttpGet("get-id")]
        public async Task <IEnumerable<int>>GetJobId(string filePath)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION2");
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "SELECT id FROM job where Filepath";
                var result = await conn.QueryAsync<int>(query, new { Filepath = filePath });
                return result;
            }
        }

        [HttpPost("add-job")]
        public async Task<IActionResult> AddJob(int userId, string filePath, double score)
        {
            try
            {
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION2");
                using (var conn = new SqlConnection(connectionString))
                {
                    var query = "INSERT INTO job (Filepath, JobStatus, Score) VALUES ( @FilePath, '0', @Score)";
                    await conn.ExecuteAsync(query, new { UserId = userId, FilePath = filePath, Score = score });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
