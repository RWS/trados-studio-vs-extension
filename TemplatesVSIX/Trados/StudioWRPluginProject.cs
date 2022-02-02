using System.Threading.Tasks;
using TemplatesVSIX.Trados.Patches;

namespace TemplatesVSIX.Trados
{
    public class StudioWRPluginProject
    {
        private readonly StudioPluginProject _project;
        
        public StudioWRPluginProject(string projectFile)
        {
            _project = new StudioPluginProject(projectFile,
                new TargetFrameworkPatch("v4.8"),
                new HintPathPatch("17"),
                new DeploymentPathPatch("17"),
                new PluginFrameworkPatch("2.0.0", "16.0.1")
            );
        }

        public Task<bool> Update()
        {
            return _project.Update();
        }
    }
}