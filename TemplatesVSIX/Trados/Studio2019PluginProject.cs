using System.Threading.Tasks;
using TemplatesVSIX.Trados.Patches;

namespace TemplatesVSIX.Trados
{
    public class Studio2019PluginProject
    {
        private readonly StudioPluginProject _project;
        
        public Studio2019PluginProject(string projectFile)
        {
            _project = new StudioPluginProject(projectFile,
                new TargetFrameworkPatch("v4.8"),
                new HintPathPatch("16"),
                new DeploymentPathPatch("16"),
                new PluginFrameworkPatch("2.0.0", "16.0.1")
            );
        }

        public Task<bool> Update()
        {
            return _project.Update();
        }
    }
}