using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TemplatesVSIX.Studio;

namespace TemplatesVSIX
{
    internal sealed class ProjectUpdateCommand
    {
        private const int CommandId = 0x0100;
        private static readonly Guid CommandSet = new Guid("8234ab98-38f0-41c9-9f72-aef849a02d39");
        private readonly IStudioContext _context;
        private readonly ProjectUpdateProgress _progress;

        public ProjectUpdateCommand(IStudioContext context)
        {
            _context = context;
            _progress = new ProjectUpdateProgress(context);
        }

        public MenuCommand InnerCommand
        {
            get
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new OleMenuCommand(Execute, menuCommandID);
                menuItem.BeforeQueryStatus += OnBeforeQueryStatus;
                return menuItem;
            }
        }

        private bool AnyProjectsSelected()
        {
            return GetSelectedProjects().Length > 0;
        }

        private async void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            if (!AnyProjectsSelected())
            {
                _progress.ReportNothingToDo();
                return;
            }

            _progress.ReportUpdateStarted();
            var count = await UpdateProjects();
            _progress.ReportUpdateComplete(count);
        }

        private string[] GetSelectedProjects()
        {
            return _context
                .GetSelectedFileNames()
                .Where(f => string.Equals(Path.GetExtension(f), ".csproj"))
                .ToArray();
        }

        private void OnBeforeQueryStatus(object sender, EventArgs e)
        {
            if (sender is OleMenuCommand command)
            {
                command.Visible = AnyProjectsSelected();
            }
        }

        private async Task<int> UpdateProjects()
        {
            var count = 0;
            foreach (var projectFile in GetSelectedProjects())
            {
                count += await UpdateProject(projectFile) ? 1 : 0;
            }

            return count;
        }

        private async Task<bool> UpdateProject(string projectFile)
        {
            _progress.ReportProjectUpdateStarted(projectFile);
            var project = new Trados.Studio2019PluginProject(projectFile);
            try
            {
                var wasUpdated = await project.Update();
                _progress.ReportProjectUpdateComplete(wasUpdated);
                return true;
            }
            catch (Exception e)
            {
                _progress.ReportErrorWhileUpdating(projectFile, e);
            }

            return false;
        }
    }
}