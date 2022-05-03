using System.Linq;
using System.Xml.Linq;

namespace TemplatesVSIX.MsBuild
{
    public class PackageReference
    {
        private readonly XElement _packageReferenceNode;

        public PackageReference(XElement packageReferenceNode)
        {
            _packageReferenceNode = packageReferenceNode;
        }

        public Include Include
        {
            get
            {
                return new Include(_packageReferenceNode.Attribute(nameof(Include)).Value);
            }
            set
            {
                _packageReferenceNode.Attribute(nameof(Include)).Value = value.ToString();
            }
        }

        public string Version
        {
            get
            {
                var node = _packageReferenceNode.Elements()
                    .FirstOrDefault(n => n.Name.LocalName == nameof(Version));

                return node == null ? "" : node.Value;
            }
            set
            {
                var node = _packageReferenceNode.Elements()
                    .FirstOrDefault(n => n.Name.LocalName == nameof(Version));

                if (node != null)
                {
                    node.Value = value;
                }
                else if (!string.IsNullOrEmpty(value))
                {
                    _packageReferenceNode.AddFirst(new XElement(nameof(Version), value));
                }
            }
        }
    }
}