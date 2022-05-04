using System.Linq;
using System.Text.RegularExpressions;
using TemplatesVSIX.MsBuild;

namespace TemplatesVSIX.Trados.Patches
{
    internal class ReferencePatch : IStudioPluginPatch
    {
        private readonly string _newVersion;

        public ReferencePatch(string newVersion)
        {
            _newVersion = newVersion;
        }

        public void PatchProject(IProject project)
        {
            if (project != null && project.References != null)
            {
                project.References
                    .Where(r => r.Include.Name.Contains("Sdl"))
                    .ToList()
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
                @"(\(ProgramFiles\)\\SDL\\SDL Trados Studio\\)Studio\d{1,2}",
                @"(MSBuildProgramFiles32)\Trados\Trados Studio\Studio" + _newVersion);

            reference.DeleteElement("Private");
            reference.DeleteElement("SpecificVersion");
        }
    }
}
