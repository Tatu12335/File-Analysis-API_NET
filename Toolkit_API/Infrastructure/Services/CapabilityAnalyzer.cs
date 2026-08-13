using Microsoft.Extensions.AI;
using System.Diagnostics;
using System.Text;
using Toolkit_API.Application.Interfaces;
using Toolkit_API.Domain.Policies;
namespace Toolkit_API.Infrastructure.Services
{
    public class CapabilityAnalyzer : ICapabilityAnalyzer
    {
        // this method should only be called from FileAnalysis class.
        public IEnumerable<Capability> DetectCapabilites(ReadOnlySpan<byte> rawData)
        {
            var capabilties = new List<Capability>();

            foreach (var rule in CapabilityRule.rules)
            {
                foreach (var signature in rule.Signature)
                {
                    if(rawData.IndexOf(Encoding.ASCII.GetBytes(signature)) >= 0)
                    {
                        capabilties.Add(rule.Capability);
                    }
                }
            }

            return capabilties;
        }
       
    }

}
