using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Toolkit_API.Application.Application_Services.Operations;
using Toolkit_API.Application.Interfaces;

namespace TASK_API.Controllers
{
    // NOTE : I will not make professional architecture,
    // just because of the time constraint and the fact that this is a test task.
    // So I will just put everything in the controller and make it work
    public class TaskController : Controller
    {
        private readonly FileScanOps _scanService;
        private readonly string _pendingJobsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Uploads_API", "pending_jobs.txt");
       

        public TaskController(FileScanOps scanService)
        {
            _scanService = scanService;
        }
        [HttpPost("scan")]
        public async Task<IActionResult> Scan(int userId, string filePath)
        {
            try
            {
                var result = await _scanService.ScanFile(filePath, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet("pending-jobs")]
        public async Task<IActionResult> GetPendingJobs()
        {
            try
            {
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION2");
                using (var conn = new SqlConnection(connectionString))
                {
                    var query = "SELECT * FROM job WHERE Jobstatus = '0'";
                    var result = await conn.QueryAsync(query);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

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
