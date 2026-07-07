using System.Diagnostics;
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
        public async Task<FileStream> ReadFile(string filePath)
        {
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            stream.Position = 0;
            return stream;
        }
        public async Task<string> SaveFileToUploadFolder(string file)
        {
            await CreateUploadFolder();
            
            using (var stream = new FileStream(Path.Combine(Environment.GetFolderPath
                (Environment.SpecialFolder.LocalApplicationData),
                "Uploads_API", Path.GetFileName(file)),
                FileMode.Create, FileAccess.Write, FileShare.None))
            {
                file = Path.GetFullPath(file);
                var charsToTrim = new char[] { '\"' };
                file = file.Trim(charsToTrim);
                Debug.WriteLine($"Saving file to upload folder: {stream.Name}");
                var result = await ReadFile(file);
                await result.CopyToAsync(stream);
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
