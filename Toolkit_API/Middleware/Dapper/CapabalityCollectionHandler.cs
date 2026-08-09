using Dapper;
using System.Data;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Middleware.Dapper
{
    public class CapabalityCollectionHandler : SqlMapper.TypeHandler<List<Capability>>
    {
        public override void SetValue(IDbDataParameter parameter, List<Capability>? value)
        {
            if (value == null || !value.Any())
            {
                parameter.Value = DBNull.Value;
                return;
            }

            parameter.Value = string.Join(",", value.Select(c => c.ToString()));

        }
        public override List<Capability> Parse(object value)
        {
            if (value is string strVal && !string.IsNullOrEmpty(strVal))
            {
                return strVal
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => Enum.TryParse<Capability>(s, out var capability) ? capability : Capability.None)
                    .Where(c => c != Capability.None)
                    .ToList();
            }
            return new List<Capability>();


        }
    }
}
