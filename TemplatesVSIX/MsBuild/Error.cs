using System.Xml.Linq;

namespace TemplatesVSIX.MsBuild
{
    internal class Error
    {
        private readonly XElement _element;

        public Error(XElement importElement)
        {
            _element = importElement;
        }

        public string Condition
        {
            get
            {
                return _element.Attribute(nameof(Condition)).Value;
            }
            set
            {
                _element.Attribute(nameof(Condition)).Value = value;
            }
        }

        public string Text
        {
            get
            {
                return _element.Attribute(nameof(Text)).Value;
            }
            set
            {
                _element.Attribute(nameof(Text)).Value = value;
            }
        }
    }
}