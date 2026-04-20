using Dapper;
using Microsoft.Data.SqlClient;
using Toolkit_API.Application.Interfaces;

namespace Toolkit_API.Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepo
    {
        public async Task<IEnumerable<string>> GetAllUsers()
        {
            var sqlQuery = "SELECT id,username,newemail FROM Users";
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("DefaultConnection")))
            {
                var users = await connection.QueryAsync<string>(sqlQuery);
                return users;
            }
        }
        public async Task<bool> CheckAdminStatus(int userId)
        {
            var sqlQuery = "SELECT roles FROM Users WHERE id = @UserId";
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("DefaultConnection")))
            {
                var role = await connection.QueryFirstOrDefaultAsync<string>(sqlQuery, new { UserId = userId });
                return role != null && role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
            }
        }
        public async Task<bool> CheckUserExists(int userId)
        {
            var sqlQuery = "SELECT COUNT(1) FROM Users WHERE id = @UserId";
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("DefaultConnection")))
            {
                var exists = await connection.QueryFirstAsync<bool>(sqlQuery, new { UserId = userId });
                return exists;
            }
        }
        public async Task<string> GetUserEmail(int userId)
        {
            var sqlQuery = "SELECT newemail FROM Users WHERE id = @UserId";
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("DefaultConnection")))
            {
                var email = await connection.QueryFirstOrDefaultAsync<string>(sqlQuery, new { UserId = userId });
                return email;
            }
        }
        public async Task<string> GetUsername(int userId)
        {
            var sqlQuery = "SELECT username FROM Users WHERE id = @UserId";
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("DefaultConnection")))
            {
                var username = await connection.QueryFirstOrDefaultAsync<string>(sqlQuery, new { UserId = userId });
                return username;
            }
        }
        public async Task<string> GetUserRole(int userId)
        {
            var sqlQuery = "SELECT roles FROM Users WHERE id = @UserId";
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("DefaultConnection")))
            {
                var role = await connection.QueryFirstOrDefaultAsync<string>(sqlQuery, new { UserId = userId });
                return role;
            }
        }
        public async Task<int> DeleteUser(int userId)
        {
            var sqlQuery = "DELETE FROM Users WHERE id = @UserId";
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("DefaultConnection")))
            {
                var affectedRows = await connection.ExecuteAsync(sqlQuery, new { UserId = userId });
                return affectedRows;
            }
        }
        public async Task<int> UpdateUserRole(int userId, string newRole)
        {
            var sqlQuery = "UPDATE Users SET roles = @NewRole WHERE id = @UserId";
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("DefaultConnection")))
            {
                var affectedRows = await connection.ExecuteAsync(sqlQuery, new { UserId = userId, NewRole = newRole });
                return affectedRows;
            }
        }
        public async Task<int> UpdateUserEmail(int userId, string newEmail)
        {
            var sqlQuery = "UPDATE Users SET newemail = @NewEmail WHERE id = @UserId";
            using (var connection = new SqlConnection(Environment.GetEnvironmentVariable("DefaultConnection")))
            {
                var affectedRows = await connection.ExecuteAsync(sqlQuery, new { UserId = userId, NewEmail = newEmail });
                return affectedRows;
            }
        }
    }
}
