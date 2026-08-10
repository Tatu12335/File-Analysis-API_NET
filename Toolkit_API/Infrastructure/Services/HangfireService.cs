using Hangfire;
using Toolkit_API.Application.Interfaces;
namespace Toolkit_API.Infrastructure.Services
{
    public class HangfireService : IhangfireService
    {
        public void storage(string connectionString)
        {
            Hangfire.GlobalConfiguration.Configuration
                .UseSqlServerStorage(connectionString)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();
        }
    }
}
