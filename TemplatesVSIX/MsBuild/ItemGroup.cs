using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TemplatesVSIX.MsBuild
{
    internal class ItemGroup
    {
        private readonly XElement _itemGroup;

        public ItemGroup(XElement itemGroup)
        {
            _itemGroup = itemGroup;
        }

        public IEnumerable<PackageReference> PackageReferences
        {
            get
            {
                return _itemGroup
                    .Descendants()
                    .Where(d => d.Name.LocalName == nameof(PackageReference))
                    .Select(e => new PackageReference(e))
                    .ToArray();
            }
        }
    }
}