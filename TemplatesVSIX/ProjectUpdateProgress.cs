using System;
using TemplatesVSIX.Studio;

namespace TemplatesVSIX
{
    internal class ProjectUpdateProgress
    {
        private readonly IStudioContext _context;

        public ProjectUpdateProgress(IStudioContext context)
        {
            _context = context;
        }

        public void ReportErrorWhileUpdating(string projectFile, Exception e)
        {
            WriteOutputLine($"ERROR: There was an error while updating the project '{projectFile}'");
            WriteOutputLine($"\t{e.Message}");
            WriteOutputLine($"\t{e.StackTrace}");
        }

        public void ReportNothingToDo()
        {
            WriteOutputLine("Nothing to update. Select a project first.");
        }

        public void ReportProjectUpdateComplete(bool wasChanged)
        {
            if (wasChanged)
                WriteOutputLine($"> Project updated. A backup of the original file was created.");
            else
                WriteOutputLine($"> No changes required");
        }

        public void ReportProjectUpdateStarted(string file)
        {
            WriteOutputLine($"Upgrading project '{file}'");
        }

        public void ReportUpdateComplete(int count)
        {
            WriteOutputLine($"Update complete, {count} project(s) updated.");
            ChangeStatus($"Update complete, {count} project(s) updated. See the output window for more information.");
        }

        public void ReportUpdateStarted()
        {
            WriteOutputLine("Update started...");
            ChangeStatus("Update started...");
        }

        private void ChangeStatus(string message) => _context.SetStatusBarText(message);

        private void WriteOutputLine(string message) => _context.WriteToOutput(message);
    }
}