using System.Collections.Generic;
using Toolkit_API.Domain.Entities.FileAnalysis;
namespace Toolkit_API.Domain.Policies
{
    public class CapabilityRuleset
    { 
        public Capability Capability { get; }
        public string[] Signature { get; }
        public CapabilityRuleset(Capability capability, string[] signature)
        {
            Capability = capability;
            Signature = signature;
        }
    }
    
}
