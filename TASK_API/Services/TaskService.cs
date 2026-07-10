using Microsoft.AspNetCore.Mvc;
using TASK_API.Domain;
using TASK_API.Services.Interfaces;
using Toolkit_API.Application.Application_Services.Operations;

namespace TASK_API.Services
{
    public class TaskService
    {
        private readonly ITaskRepo _repository;
        private readonly FileScanOps _operations;
        public TaskService(ITaskRepo repository, FileScanOps operations)
        {
            _repository = repository;
            _operations = operations;
        }
        public async Task Add_Folder(int userId, string folderPath)
        {
            var files = await _repository.GetPendingJobs();
          
            if (files == null || !files.Any())
            {    
                return;
            }

            foreach (var file in files)
            {
                await _repository.UpdateJobStatusProcessing(file.filePath);
                var result = await _operations.ScanFile(file.filePath, userId);
                await _repository.UpdateJobStatusCompleted(file.filePath);
            }

        }
        public async Task Add_Job(string filePath)
        {
            await _repository.AddJob(filePath);
        }
        public async Task<string> Scan(int userId)
        {
            
                var result = string.Empty;
                var jobs = await _repository.GetPendingJobs();

                

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
                

                return result;
            
           
        }
    }
}
