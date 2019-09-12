using System.Xml.Linq;

namespace TemplatesVSIX.MsBuild
{
    internal class Import
    {
        private readonly XElement _element;

        public Import(XElement importElement)
        {
            _element = importElement;
        }

        public string Condition
        {
            get
            {
                XAttribute attribute = _element.Attribute(nameof(Condition));
                if (attribute != null)
                {
                    return attribute.Value;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                XAttribute attribute = _element.Attribute(nameof(Condition));
                if (attribute != null)
                {
                    attribute.Value = value;
                }
            }
        }

        public string Project
        {
            get
            {
                return _element.Attribute(nameof(Project)).Value;
            }
            set
            {
                _element.Attribute(nameof(Project)).Value = value;
            }
        }
    }
}