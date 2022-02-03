using TemplatesVSIX.MsBuild;

namespace TemplatesVSIX.Trados.Patches
{
    internal class DeploymentPathPatch : IStudioPluginPatch
    {
        private readonly string _newVersion;

        public DeploymentPathPatch(string newVersion)
        {
            _newVersion = newVersion;
        }

        public void PatchProject(IProject project)
        {
            project.AddProperty(
                "PluginDeploymentPath",
                $@"$(AppData)\Trados\Trados Studio\{_newVersion}\Plugins");
        }

        public void PatchPackages(IPackagesConfig packageConfig)
        {
        }
    }
}