using Microsoft.AspNetCore.Mvc;
using TASK_API.Domain;
using TASK_API.Services.Interfaces;
using Toolkit_API.Application.Application_Services.Operations;

namespace TASK_API.Services
{
    public class ScanService
    {
        private readonly ITaskRepo _repository;
        private readonly FileScanOps _operations;
        public ScanService(ITaskRepo repository, FileScanOps operations)
        {
            _repository = repository;
            _operations = operations;
        }

        public async Task<string> Scan(int userId, string filePath)
        {
            
                var result = string.Empty;
                var jobs = await _repository.GetPendingJobs();

                if (jobs == null || !jobs.Any())
                {

                    result = await _operations.ScanFile(filePath, userId);

                    return result;
                }

                foreach (var job in jobs)
                {

                    var jobId = await _repository.GetJobId(job.filePath);

                    if (jobId != null)
                    {
                        await _repository.UpdateJobStatusProcessing(job.filePath);
                        result = await _operations.ScanFile(job.filePath, userId);
                        await _repository.UpdateJobStatusCompleted(job.filePath);

                    }
                }
                await _repository.UpdateJobStatusCompleted(filePath);

                return result;
            
           
        }
    }
}
