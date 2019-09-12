using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemplatesVSIX.MsBuild
{
    internal interface IPackagesConfig
    {
        IEnumerable<Package> Packages { get; }
    }
}