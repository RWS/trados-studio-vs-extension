using TemplatesVSIX.MsBuild;

namespace TemplatesVSIX.Trados
{
    interface IStudioPluginPatch
    {
        void PatchProject(IProject project);

        void PatchPackages(IPackagesConfig packageConfig);
    }
}
