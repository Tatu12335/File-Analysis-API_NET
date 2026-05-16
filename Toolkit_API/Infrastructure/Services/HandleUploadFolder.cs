using Toolkit_API.Application.Interfaces;
namespace Toolkit_API.Infrastructure.Services
{
    public class HandleUploadFolder : IHandleUploadFolder
    {
        
        public async Task CreateUploadFolder()
        {
            var uploadFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Uploads_API");
            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }
        }
        public async Task ReadFile(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                
            }
        }
        public async Task<string> SaveFileToUploadFolder(IFormFile file)
        {
            await CreateUploadFolder();
            using (var stream = new FileStream(Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData),
                "Uploads_API", file.FileName),
                FileMode.Create, FileAccess.Write, FileShare.None))
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
                FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return stream.Name;
            }
        }
    }
}
