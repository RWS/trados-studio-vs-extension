using TemplatesVSIX.MsBuild;

namespace TemplatesVSIX.Trados.Patches
{
    class TargetFrameworkPatch : IStudioPluginPatch
    {
        private readonly string _framework;

        public TargetFrameworkPatch(string framework)
        {
            _framework = framework;
        }

        public void PatchPackages(IPackagesConfig packageConfig)
        {
        }

        public void PatchProject(IProject project)
        {
            project.TargetVersion = _framework;
        }
    }
}
