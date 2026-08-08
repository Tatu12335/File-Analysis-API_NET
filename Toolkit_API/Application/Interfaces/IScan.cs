using Hangfire.Server;

namespace Toolkit_API.Application.Interfaces
{
    public interface IScan
    {
        public Task ScanFile(string filePath, int userId, PerformContext context = null!);
    }
}
