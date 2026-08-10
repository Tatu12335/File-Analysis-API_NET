using Dapper;
using Microsoft.Data.SqlClient;
using TASK_API.Domain;
using TASK_API.Services.Interfaces;

namespace TASK_API.Repositories
{
    public class TaskRepo : ITaskRepo
    {
        public async Task<IEnumerable<Job>> GetPendingJobs()
        {

            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION2");
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "SELECT Filepath FROM job WHERE Jobstatus = '0'";
                var result = await conn.QueryAsync<Job>(query);
                return result;
            }


        }

        public async Task UpdateJobStatusProcessing(string filepath)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION2");
            var id = await GetJobId(filepath);

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

            string query = "Update job Set Jobstatus = 2 Where id = @Id";
            using (var conn = new SqlConnection(connectionString))
            {
                await conn.ExecuteAsync(query, new { Id = id });
            }
        }
        public async Task<int?> GetJobId(string filePath)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION2");
            using (var conn = new SqlConnection(connectionString))
            {
                var query = "SELECT top 1 id FROM job where Filepath = @FilePath";
                // Using QueryFirstOrDefaultAsync to get the first matching record or null if none found.
                return await conn.QueryFirstOrDefaultAsync<int?>(query, new { FilePath = filePath });
            }
        }
        public async Task AddJob(string filePath, int userId)
        {
            string query = "INSERT INTO job (Filepath, JobStatus, score, userId) VALUES (@FilePath, '0', 0.0, @UserId)";

            using (var conn = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION2")))
            {
                await conn.ExecuteAsync(query, new { FilePath = filePath, UserId = userId });
            }
        }
        public async Task UpdateJobStatusFailed(string filepath)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION2");
            var id = await GetJobId(filepath);
            string query = "Update job Set Jobstatus = 3 Where id = @Id";
            using (var conn = new SqlConnection(connectionString))
            {
                await conn.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
