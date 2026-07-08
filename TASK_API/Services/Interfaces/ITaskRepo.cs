using TASK_API.Domain;

namespace TASK_API.Services.Interfaces
{
    public interface ITaskRepo
    {
        public Task AddJob(int userId, string filePath);
        public Task<int?> GetJobId(string filePath);
        public Task UpdateJobStatusCompleted(string filepath);
        public Task UpdateJobStatusFailed(string filepath);
        public Task UpdateJobStatusProcessing(string filepath);
        public Task<IEnumerable<Job>> GetPendingJobs();
    }
}
