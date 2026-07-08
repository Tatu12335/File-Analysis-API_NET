using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TASK_API.Domain;
using TASK_API.Services;
using Toolkit_API.Application.Application_Services.FileOperations;
using Toolkit_API.Application.Application_Services.Operations;

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
        private readonly ScanService _scanService2;


        // this whole controller is all over the fucking place :(
        public TaskController(FileScanOps scanService, HandleFolder handleFolder, ScanService scanService2)
        {
            _scanService = scanService;
            _handleFolder = handleFolder;
            _scanService2 = scanService2;
        }
        // Also Note that for filescanning you should use this endpoint because this has the [ wip ] background workers 
        // Also i know i could have use hangfire but i decided to try do the background worker myself!
        [HttpPost("scan")]
        public async Task<IActionResult> Scan([FromBody] ScanDTO scan)
        {
            var result = await _scanService2.Scan(scan.userId, scan.filePath);
            return Ok(result);
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
                    await AddJob(userId, file);
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
        public async Task UpdateJobStatusProcessing(string filepath)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION2");
            var id = await GetJobId(filepath);

            if (id == null)
                return;

            string query = "Update job Set Jobstatus = 1 Where id = @Id";
            using (var conn = new SqlConnection(connectionString))
            {
                await conn.ExecuteAsync(query, new { Id = id });
            }
        }
        public async Task UpdateJobStatusCompleted(string filepath)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION2");
            var id = await GetJobId(filepath);
            if (id == null)
                return;
            string query = "Update job Set Jobstatus = 2 Where id = @Id";
            using (var conn = new SqlConnection(connectionString))
            {
                await conn.ExecuteAsync(query, new { Id = id });
            }
        }


        [HttpGet("get-id")]
        public async Task<int?> GetJobId(string filePath)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION2");
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "SELECT top 1 id FROM job where Filepath = @FilePath";

                return await conn.QueryFirstOrDefaultAsync<int?>(query, new { FilePath = filePath });
            }
        }

        [HttpPost("add-job")]
        public async Task<IActionResult> AddJob(int userId, string filePath)
        {
            try
            {
                var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION2");
                using (var conn = new SqlConnection(connectionString))
                {
                    var query = "INSERT INTO job (Filepath, JobStatus, score) VALUES (@FilePath, '0', 0.0)";
                    await conn.ExecuteAsync(query, new { FilePath = filePath });
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
