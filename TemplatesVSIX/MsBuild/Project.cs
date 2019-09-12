using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TemplatesVSIX.MsBuild
{
    internal class Project : IProject, ISalvageable
    {
        public static readonly XNamespace defaultNs = "http://schemas.microsoft.com/developer/msbuild/2003";
        protected readonly XDocument _document;
        private const string PropertyGroupText = "PropertyGroup";
        private const string TargetFrameworkVersionText = "TargetFrameworkVersion";

        public Project(string content)
            : this(XDocument.Parse(content, LoadOptions.PreserveWhitespace))
        {
        }

        public Project(XDocument document)
        {
            _document = document;
            _document.Declaration = new XDeclaration("1.0", "utf-8", null);
        }

        public IEnumerable<Error> Errors
        {
            get
            {
                return _document
                    .Descendants()
                    .Where(d => d.Name.LocalName == nameof(Error))
                    .Select(e => new Error(e))
                    .ToArray();
            }
        }

        public IEnumerable<Import> Imports
        {
            get
            {
                return _document
                    .Descendants()
                    .Where(d => d.Name.LocalName == nameof(Import))
                    .Select(i => new Import(i))
                    .ToArray();
            }
        }

        public IEnumerable<IReference> References
        {
            get
            {
                return _document
                    .Descendants()
                    .Where(d => d.Name.LocalName == nameof(Reference))
                    .Select(r => new Reference(r))
                    .ToArray();
            }
        }

        public string TargetVersion
        {
            get
            {
                return _document
                        .Descendants()
                        .First(d => d.Name.LocalName == TargetFrameworkVersionText)
                        .Value;
            }
            set
            {
                var target = _document
                    .Descendants()
                    .First(d => d.Name.LocalName == TargetFrameworkVersionText);

                target.Value = value;
            }
        }

        public static async Task<IProject> LoadAsync(string path)
        {
            using (var stream = new StreamReader(path))
            {
                var content = await stream.ReadToEndAsync();
                return new Project(content);
            }
        }

        public void AddProperty(string name, string value)
        {
            var target = _document
                .Descendants()
                .FirstOrDefault(d => d.Name.LocalName == name);

            if (target != null)
            {
                target.Value = value;
            }
            else
            {
                var group = _document
                    .Descendants()
                    .First(d => d.Name.LocalName == PropertyGroupText && d.Attribute("Condition") == null);
                group.Add(new XElement(defaultNs + name, value));
            }
        }

        public virtual async Task<bool> SaveAsync(string path)
        {
            using (var stream = new StreamWriter(new FileStream(path, FileMode.Create), new UTF8Encoding(false)))
            {
                await stream.WriteAsync(_document.Declaration.ToString());
                await stream.WriteAsync(_document.ToString());
            }

            return true;
        }

        public override string ToString()
        {
            return _document.ToString();
        }
    }
}