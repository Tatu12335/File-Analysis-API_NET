using Dapper;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Text.Json;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Entities.Files;

namespace Toolkit_API.Infrastructure.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly string _connectionString;

        public ResultRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<ScanResult?> GetResultAsync(string jobId)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                Debug.WriteLine($"Getting result for jobId: {jobId}");
                string query = "SELECT JsonData FROM ScanResult WHERE jobId = @JobId";
                string jsonData = await connection.QuerySingleAsync<string>(query, new { JobId = jobId });

               

                return JsonSerializer.Deserialize<ScanResult>(jsonData);

            }

        }
        public async Task SaveResultAsync(string jobId, ScanResult result)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string jsonData = JsonSerializer.Serialize(result);
                Debug.WriteLine($"Saving result for jobId: {jobId}, jsonData: {jsonData}");
                
                string query = "INSERT INTO ScanResult (jobId, JsonData) VALUES (@JobId, @JsonData)";
                await connection.ExecuteAsync(query, new { JobId = jobId, JsonData = jsonData });
            }
        }
    }
}
