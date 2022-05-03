using System;
using System.Linq;
using TemplatesVSIX.MsBuild;

namespace TemplatesVSIX.Trados.Patches
{
    internal class PluginFrameworkPatch : IStudioPluginPatch
    {
        private const StringComparison comparision = StringComparison.InvariantCultureIgnoreCase;
        private readonly string _pluginFrameworkBuildVersion;
        private readonly string _pluginFrameworkVersion;

        public PluginFrameworkPatch(string pluginFrameworkVersion, string pluginFrameworkBuildVersion)
        {
            _pluginFrameworkVersion = pluginFrameworkVersion;
            _pluginFrameworkBuildVersion = pluginFrameworkBuildVersion;
        }

        public void PatchPackages(IPackagesConfig packageConfig)
        {
            var pluginFramework = packageConfig.Packages
                .FirstOrDefault(r => r.Id.StartsWith("Sdl.Core.PluginFramework", comparision));
            var pluginFrameworkBuild = packageConfig.Packages
                .FirstOrDefault(r => r.Id.StartsWith("Sdl.Core.PluginFramework.Build", comparision));

            if (pluginFramework != null)
                pluginFramework.Version = _pluginFrameworkVersion;

            if (pluginFrameworkBuild != null)
                pluginFrameworkBuild.Version = _pluginFrameworkBuildVersion;
        }

        public void PatchProject(IProject project)
        {
            var itemGroups = project.ItemGroups.ToList();
            var pluginFrameworkPackageReferences =
                itemGroups
                .Where(ig => ig.PackageReferences
                .Any(pr => pr.Include.Name.Contains("Sdl.Core.PluginFramework")))
                .SelectMany(ig => ig.PackageReferences);

            foreach (var reference in pluginFrameworkPackageReferences)
            {
                reference.Version =
                    reference.Include.Name == "Sdl.Core.PluginFramework" ?
                    _pluginFrameworkVersion : _pluginFrameworkBuildVersion;
            }
        }
    }
}