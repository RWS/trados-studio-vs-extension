using System.IO;
using System.Threading.Tasks;
using TemplatesVSIX.MsBuild;

namespace TemplatesVSIX.Trados
{
    internal class StudioPluginProject
    {
        private readonly string _projectFile;
        private readonly string _packagesConfig;
        private readonly IStudioPluginPatch[] _patches;

        public StudioPluginProject(
            string projectFile,
            params IStudioPluginPatch[] patches)
        {
            _projectFile = projectFile;
            _packagesConfig = Path.Combine(Path.GetDirectoryName(projectFile), "packages.config");
            _patches = patches;
        }

        public virtual async Task<bool> Update()
        {
            var project = await UpdateProject();
            var projectSalvageable = new Backup(project as ISalvageable);
            var projectSaveResult = await projectSalvageable.SaveAsync(_projectFile);

            var packagesSaveResult = true;
            if (File.Exists(_packagesConfig))
            {
                var packagesConfig = await UpdatePackagesConfig();
                var packagesConfigSalvageable = new Backup(packagesConfig as ISalvageable);
                packagesSaveResult = await packagesConfigSalvageable.SaveAsync(_packagesConfig);
            }

            return projectSaveResult || packagesSaveResult;
        }

        private async Task<IPackagesConfig> UpdatePackagesConfig()
        {
            var packagesConfig = await PackagesConfig.LoadAsync(_packagesConfig);
            foreach (var patch in _patches)
            {
                patch.PatchPackages(packagesConfig);
            }
            return packagesConfig;
        }

        private async Task<IProject> UpdateProject()
        {
            var project = await Project.LoadAsync(_projectFile);
            foreach (var patch in _patches)
            {
                patch.PatchProject(project);
            }

            return project;
        }
    }
}