using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.AspNetCore.SignalR;

namespace Toolkit_API.Application.Hangfire
{
    public class Scanhub : Hub
    { 
        public async Task JoinScanGroup(string jobId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, jobId);
        }
    }
}
