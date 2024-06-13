using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
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
#pragma warning disable VSTHRD109 // Switch instead of assert in async methods
            ThreadHelper.ThrowIfNotOnUIThread();
#pragma warning restore VSTHRD109 // Switch instead of assert in async methods

            if (!AnyProjectsSelected())
            {
                _progress.ReportNothingToDo();
                return;
            }

            _progress.ReportUpdateStarted();
            var count = await UpdateProjectsAsync();
            _progress.ReportUpdateComplete(count);
        }

        private string[] GetSelectedProjects()
        {
            return _context
                .GetSelectedFileNames()
                .Where(f => string.Equals(Path.GetExtension(f), ".csproj"))
                .ToArray();
        }

        private List<string> GetSelectedProjectsManifests()
        {
            var projectPaths = _context
                .GetSelectedFileNames();

            var pluginManifests = new List<string>();
            projectPaths
                .ToList()
                .ForEach(f => pluginManifests.Add(Regex.Replace(f, Path.GetFileName(f), "pluginpackage.manifest.xml")));

            return pluginManifests;
        }

        private void OnBeforeQueryStatus(object sender, EventArgs e)
        {
            if (sender is OleMenuCommand command)
            {
                command.Visible = AnyProjectsSelected();
            }
        }

        private async Task<int> UpdateProjectsAsync()
        {
            var count = 0;
            foreach (var projectFile in GetSelectedProjects())
            {
                count += await UpdateProjectAsync(projectFile) ? 1 : 0;
            }

            foreach (var manifest in GetSelectedProjectsManifests())
            {
                count += await UpdatePluginManifestAsync(manifest) ? 1 : 0;
            }

            return count;
        }

        private async Task<bool> UpdatePluginManifestAsync(string manifest)
        {
            _progress.ReportProjectUpdateStarted(manifest);
            try
            {
                var document = await GetPluginManifestAsync(manifest);
                UpdateDocument(document);
                await WritePluginManifestAsync(manifest, document);

                return true;
            }
            catch (Exception e)
            {
                _progress.ReportErrorWhileUpdating(manifest, e);
            }

            return false;
        }

        private static async Task WritePluginManifestAsync(string manifest, XDocument document)
        {
            using (var stream = new StreamWriter(new FileStream(manifest, FileMode.Create), new UTF8Encoding(false)))
            {
                await stream.WriteAsync(document.ToString());
            }
        }

        private static void UpdateDocument(XDocument document)
        {
            var requiredProductElement =
                                document
                                    .Descendants()
                                    .FirstOrDefault(d => d.Name.LocalName == "RequiredProduct");

            SetAttributeValue(requiredProductElement, "name", "TradosStudio");
            SetAttributeValue(requiredProductElement, "minversion", "18.0");
            SetAttributeValue(requiredProductElement, "maxversion", "18.9");

        }

        private static void SetAttributeValue(XElement element, string attribute, string value)
        {
            if (element == null) return;

            if (element.Attribute(attribute) == null) element.Add(new XAttribute(attribute, value));
            else element.Attribute(attribute).Value = value;
        }

        private static async Task<XDocument> GetPluginManifestAsync(string manifest)
        {
            XDocument document;
            using (var stream = new StreamReader(manifest))
            {
                var content = await stream.ReadToEndAsync();
                document = XDocument.Parse(content, LoadOptions.PreserveWhitespace);
            }

            return document;
        }

        private async Task<bool> UpdateProjectAsync(string projectFile)
        {
            _progress.ReportProjectUpdateStarted(projectFile);
            var project = new Trados.StudioWRPluginProject(projectFile);
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