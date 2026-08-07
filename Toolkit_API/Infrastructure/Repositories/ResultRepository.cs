using Dapper;
using Microsoft.Data.SqlClient;
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
                string query = "SELECT JsonData FROM ScanResult WHERE JobId = @JobId";
                string jsonData = await connection.QueryFirstOrDefaultAsync<string>(query, new { JobId = jobId });

                return JsonSerializer.Deserialize<ScanResult>(jsonData);

            }

        }
        public async Task SaveResultAsync(string jobId, ScanResult result)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string jsonData = JsonSerializer.Serialize(result);
                string query = "INSERT INTO ScanResult (JobId, JsonData) VALUES (@JobId, @JsonData)";
                await connection.ExecuteAsync(query, new { JobId = jobId, JsonData = jsonData });
            }
        }
    }
}
