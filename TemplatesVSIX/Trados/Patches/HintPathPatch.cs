using System.Linq;
using System.Text.RegularExpressions;
using TemplatesVSIX.MsBuild;

namespace TemplatesVSIX.Trados.Patches
{
    internal class HintPathPatch : IStudioPluginPatch
    {
        private readonly string _newVersion;

        public HintPathPatch(string newVersion)
        {
            _newVersion = newVersion;
        }

        public void PatchProject(IProject project)
        {
            if (project != null && project.References != null)
            {
                project.References.ToList()
                    .ForEach(UpdateHintPath);
            }
        }

        public void PatchPackages(IPackagesConfig packageConfig)
        {
        }

        private void UpdateHintPath(IReference reference)
        {
            reference.HintPath = Regex.Replace(
                reference.HintPath,
                @"(\\SDL\\SDL Trados Studio\\)Studio\d{1,2}",
                "$1Studio" + _newVersion);
        }
    }
}
