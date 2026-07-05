using Microsoft.VisualBasic;
using Toolkit_API.Application.Application_Services.Operations;
using Toolkit_API.Application.Interfaces;

namespace Toolkit_API.Application.Application_Services.FileOperations
{
    
    public class HandleAPICall
    {
        private readonly ICallExternalAPI _callAPI;
        private readonly HandleResult _result;
        public HandleAPICall(ICallExternalAPI callAPI, HandleResult handleResult)
        {
            _callAPI = callAPI;
            _result = handleResult;
        }

        public async Task callAPI(byte[] hash, string envVar)
        {
            var callResult = await _callAPI.CallAPI(hash, envVar);
            
            
        }
    }
}
