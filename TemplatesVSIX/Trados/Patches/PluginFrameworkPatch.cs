using System;
using System.Linq;
using System.Text.RegularExpressions;
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

            bool ContainsPluginFrameworkBuild(string text) =>
                text.IndexOf("Sdl.Core.PluginFramework.Build", comparision) >= 0;

            project.References
                .Where(r => r.Include.Name.StartsWith("Sdl.Core.PluginFramework", comparision))
                .ToList()
                .ForEach(UpdateCorePluginFrameworkReferences);

            project.Imports
                .Where(i => ContainsPluginFrameworkBuild(i.Project))
                .ToList()
                .ForEach(UpdateCorePluginFrameworkImports);

            project.Errors
                .Where(e => ContainsPluginFrameworkBuild(e.Condition))
                .ToList()
                .ForEach(UpdateCorePluginFrameworkErrors);
        }

        private string ReplacePluginFrameworkBuildVersion(string toBeReplaced)
        {
            return Regex.Replace(
                            toBeReplaced,
                            @"(Sdl\.Core\.PluginFramework\.Build)\.\d{1,2}\.\d{1,2}\.\d{1,2}",
                            "$1." + _pluginFrameworkBuildVersion);
        }

        private void UpdateCorePluginFrameworkErrors(Error error)
        {
            error.Condition = ReplacePluginFrameworkBuildVersion(error.Condition);
            error.Text = ReplacePluginFrameworkBuildVersion(error.Text);
        }

        private void UpdateCorePluginFrameworkImports(Import import)
        {
            import.Project = ReplacePluginFrameworkBuildVersion(import.Project);
            import.Condition = ReplacePluginFrameworkBuildVersion(import.Condition);
        }

        private void UpdateCorePluginFrameworkReferences(IReference reference)
        {
            reference.Include = new Include(reference.Include.Name);
            reference.HintPath = Regex.Replace(
                reference.HintPath,
                @"(Sdl\.Core\.PluginFramework)\.\d\.\d\.\d",
                "$1." + _pluginFrameworkVersion);
        }
    }
}