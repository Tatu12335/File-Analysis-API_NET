using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TASK_API.Domain;
using TASK_API.Services;
using Toolkit_API.Application.Application_Services.FileOperations;
using Toolkit_API.Application.Application_Services.Operations;
using TASK_API.Services.Interfaces;
using System.Security.Claims;

namespace TASK_API.Controllers
{
    
    public class TaskController : Controller
    {
        private readonly FileScanOps _scanService;

        private readonly HandleFolder _handleFolder;
        private readonly TaskService _scanService2;


        
        public TaskController(FileScanOps scanService, HandleFolder handleFolder, TaskService scanService2)
        {
            _scanService = scanService;
            _handleFolder = handleFolder;
            _scanService2 = scanService2;
        }
        
        [HttpPost("scan")]
        public async Task<IActionResult> Scan([FromBody] ScanDTO scan)
        {
            var result = await _scanService2.Scan(scan.userId);
            return Ok(result);
        }
        [HttpPost("add-folder")]
        public async Task<IActionResult> AddFolder(int userId, string folderPath)
        {
            await _scanService2.Add_Folder(userId, folderPath);
            return Ok();
        }
        

       /* [HttpPost("add-job")]
        public async Task<IActionResult> AddJob(string filePath)
        {
            //This is for whenever i make the frontend 
            //var userId  = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _scanService2.Add_Job(filePath, 2025);
            return Ok();
        }*/
    }
}
