using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TemplatesVSIX.MsBuild
{
    internal class PackagesConfig : IPackagesConfig, ISalvageable
    {
        private readonly XDocument _document;

        public PackagesConfig(string content)
            : this(XDocument.Parse(content, LoadOptions.PreserveWhitespace))
        {
        }

        public PackagesConfig(XDocument document)
        {
            _document = document;
        }

        public IEnumerable<Package> Packages
        {
            get
            {
                return _document
                    .Descendants()
                    .Where(d => d.Name.LocalName == "package")
                    .Select(e => new Package(e))
                    .ToArray();
            }
        }

        public static async Task<IPackagesConfig> LoadAsync(string path)
        {
            using (var stream = new StreamReader(path))
            {
                var content = await stream.ReadToEndAsync();
                return new PackagesConfig(content);
            }
        }

        public virtual async Task<bool> SaveAsync(string path)
        {
            using (var stream = new StreamWriter(new FileStream(path, FileMode.Create), new UTF8Encoding(false)))
            {
                await stream.WriteAsync(_document.ToString());
            }

            return true;
        }
    }
}