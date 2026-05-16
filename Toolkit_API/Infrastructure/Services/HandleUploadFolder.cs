using Toolkit_API.Application.Interfaces;
using Microsoft.AspNetCore.Http;
namespace Toolkit_API.Infrastructure.Services
{
    public class HandleUploadFolder : IHandleUploadFolder
    {
      
        public async Task<string> SaveFileToUploadFolder(IFormFile file)
        {
            using (var stream = new FileStream(Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), 
                "Uploads_API", file.FileName), 
                FileMode.Create,FileAccess.Write,FileShare.None))
            {
                
                await file.CopyToAsync(stream);
                return stream.Name;
            }
        }

        public async Task<string> GetFilePath(string fileName)
        {
            using (var stream = new FileStream(Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData), 
                "Uploads_API", fileName), 
                FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
            {
                return stream.Name;
            }
        }
    }
}
