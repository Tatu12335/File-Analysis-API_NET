using Dapper;
using System.Data;
using Toolkit_API.Domain.Policies;

namespace Toolkit_API.Middleware.Dapper
{
    public class CapabalityCollectionHandler : SqlMapper.TypeHandler<IEnumerable<Capability>>
    {
        public override void SetValue(IDbDataParameter parameter, IEnumerable<Capability>? value)
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
                    .Select(s => Enum.Parse<Capability>(s.Trim(), ignoreCase: true))
                    .ToList();
            }
            return new List<Capability>();


        }
    }
}
