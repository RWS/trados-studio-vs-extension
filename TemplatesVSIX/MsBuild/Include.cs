using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplatesVSIX.MsBuild
{
    public class Include
    {
        public Include(string includeString)
        {
            var items = includeString.Split(',')
                .Where(s => !string.IsNullOrWhiteSpace(s));

            if (!items.Any())
                throw new ArgumentException("The include string is invalid", nameof(includeString));

            Name = items.First();
            Properties = items.Skip(1)
                .Select(s =>
                {
                    var parts = s.Split('=');
                    return new
                    {
                        Key = parts.Length > 0 ? parts[0].Trim() : "",
                        Value = parts.Length > 1 ? parts[1].Trim() : ""
                    };
                })
                .ToDictionary(s => s.Key, s => s.Value);
        }

        public string Name { get; }

        public Dictionary<string, string> Properties { get; }

        public override string ToString()
        {
            return Name +
                Properties
                    .Aggregate("", (result, item) => result + $"{item.Key}={item.Value}, ")
                    .Trim(", ".ToArray());
        }
    }
}
