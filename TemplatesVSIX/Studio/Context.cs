using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TemplatesVSIX.Studio
{
    class Context : IStudioContext
    {
        private readonly IVsOutputWindowPane _outputWindowPane;
        private readonly DTE2 _dte;

        public Context(DTE2 dte, IVsOutputWindowPane outputWindowPane)
        {
            _dte = dte;
            _outputWindowPane = outputWindowPane;
        }

        public IEnumerable<string> GetSelectedFileNames()
        {
            var items = (Array)_dte.ToolWindows.SolutionExplorer.SelectedItems;
            return items.Cast<UIHierarchyItem>()
                .Select(item => item.Object)
                .OfType<Project>()
                .Where(p => p.Properties != null)
                .Select(p => p.FileName)
                .ToArray();
        }

        public void SetStatusBarText(string text)
        {
            _dte.StatusBar.Text = text;
        }

        public void WriteToOutput(string message)
        {
            try
            {
                ThreadHelper.Generic.BeginInvoke(() =>
                    _outputWindowPane.OutputStringThreadSafe(message + Environment.NewLine));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
        }
    }
}
