using Microsoft.AspNetCore.SignalR;
using Toolkit_API.Application.Analysis;

namespace Toolkit_API.Application.Hangfire
{
    public class ScanJob
    {
        private readonly IHubContext<Scanhub> _hubContext;
        private readonly StaticScan _staticScan;
        public ScanJob(IHubContext<Scanhub> hubContext, StaticScan staticScan)
        {
            _hubContext = hubContext;
            _staticScan = staticScan;
        }
        public async Task ExecuteScan(string filePath, int userId,string connectionId)
        {

            var scanResult = await _staticScan.ScanFile(filePath, userId);

            await _hubContext.Clients.Client(connectionId)
                .SendAsync("ReceiveScanResult", scanResult);
        }
    }
}
