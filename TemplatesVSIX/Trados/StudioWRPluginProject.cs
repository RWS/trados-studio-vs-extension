﻿using System.Threading.Tasks;
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
                new PluginFrameworkPatch("2.1.0", "16.1.0")
            );
        }

        public Task<bool> Update()
        {
            return _project.UpdateAsync();
        }
    }
}