using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvToolKitWPF.Services.Interfaces
{
    public interface ILoginService
    {
        public Task<string> Auth(string username, string password);
    }
}
