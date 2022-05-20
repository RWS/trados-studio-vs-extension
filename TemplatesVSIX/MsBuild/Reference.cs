using System.Linq;
using System.Xml.Linq;

namespace TemplatesVSIX.MsBuild
{
    internal class Reference : IReference
    {
        private readonly XElement _referenceElement;

        public Reference(XElement referenceElement)
        {
            _referenceElement = referenceElement;
        }

        public Include Include
        {
            get
            {
                return new Include(_referenceElement.Attribute(nameof(Include)).Value);
            }
            set
            {
                _referenceElement.Attribute(nameof(Include)).Value = value.ToString();
            }
        }

        public void DeleteReference()
        {
            _referenceElement.Remove();
        }

        public void DeleteElement(string name)
        {
            _referenceElement
                .Elements()
                .FirstOrDefault(n => n.Name.LocalName == name)
                ?.Remove();
        }

        public string HintPath
        {
            get
            {
                var node = _referenceElement.Elements()
                    .FirstOrDefault(n => n.Name.LocalName == nameof(HintPath));

                return node == null ? "" : node.Value;
            }
            set
            {
                var node = _referenceElement.Elements()
                    .FirstOrDefault(n => n.Name.LocalName == nameof(HintPath));

                if (node != null)
                {
                    node.Value = value;
                }
                else if (!string.IsNullOrEmpty(value))
                {
                    _referenceElement.AddFirst(new XElement(Project.defaultNs + nameof(HintPath), value));
                }
            }
        }
    }
}
