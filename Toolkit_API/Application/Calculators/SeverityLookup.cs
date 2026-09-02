using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Application.Calculators
{
    public static class SeverityLookup
    {
        private static readonly Dictionary<Capability, double> BaseSeverity = new()
        {
            [Capability.ProcessInjection] = 1.0,
            [Capability.Keylogging] = 1.0,
            [Capability.AntiVM] = 0.3,
            [Capability.CommandExecution] = 0.2,
            [Capability.CommandLineExecution] = 0.2,
            [Capability.AntiDebug] = 0.3,

        };
        public static double GetBaseSeverity(Capability cap) =>
            BaseSeverity.GetValueOrDefault(cap, defaultValue: 0.0);
    }
}
