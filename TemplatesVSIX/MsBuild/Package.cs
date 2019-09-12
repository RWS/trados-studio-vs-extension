using System.Xml.Linq;

namespace TemplatesVSIX.MsBuild
{
    internal class Package
    {
        private readonly XElement _packageNode;

        public Package(XElement packageNode)
        {
            _packageNode = packageNode;
        }

        public string Id
        {
            get => _packageNode.Attribute("id").Value;
            set => _packageNode.Attribute("id").Value = value;
        }

        public string Version
        {
            get => _packageNode.Attribute("version").Value;
            set => _packageNode.Attribute("version").Value = value;
        }

        public string TargetFramework
        {
            get => _packageNode.Attribute("targetFramework").Value;
            set => _packageNode.Attribute("targetFramework").Value = value;
        }
    }
}
