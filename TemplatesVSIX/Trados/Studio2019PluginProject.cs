using System.Threading.Tasks;
using TemplatesVSIX.Trados.Patches;

namespace TemplatesVSIX.Trados
{
    public class Studio2019PluginProject
    {
        private StudioPluginProject _project;

        public Studio2019PluginProject(string projectFile)
        {
            _project = new StudioPluginProject(projectFile,
                new TargetFrameworkPatch("v4.7"),
                new HintPathPatch("15"),
                new DeploymentPathPatch("15"),
                new PluginFrameworkPatch("1.8.0", "15.1.0")
            );
        }

        public Task<bool> Update()
        {
            return _project.Update();
        }
    }
}