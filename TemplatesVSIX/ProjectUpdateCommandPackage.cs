using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using TemplatesVSIX.Studio;
using Task = System.Threading.Tasks.Task;

namespace TemplatesVSIX
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExistsAndFullyLoaded_string, PackageAutoLoadFlags.BackgroundLoad)]
    [InstalledProductRegistration("#111", "#113", "1.1", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuidString)]
    public sealed class ProjectUpdateCommandPackage : AsyncPackage
    {
        private ProjectUpdateCommand _projectUpdateCommand;

        public const string PackageGuidString = "e5f863ce-19ae-4f6c-98bc-bd3d644eeb11";
        public const string OutputWindowPaneName = "SDL Templates";

        public ProjectUpdateCommandPackage()
        {
        }

        #region Package Members

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await base.InitializeAsync(cancellationToken, progress);

            var dte = (DTE2)GetService(typeof(DTE));
            var commandService = (OleMenuCommandService)await GetServiceAsync(typeof(IMenuCommandService));

            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            var guid = Guid.NewGuid();
            var output = (IVsOutputWindow)await GetServiceAsync(typeof(SVsOutputWindow));
            output.CreatePane(ref guid, OutputWindowPaneName, 1, 1);
            output.GetPane(ref guid, out IVsOutputWindowPane pane);

            var context = new Context(dte, pane);
            _projectUpdateCommand = new ProjectUpdateCommand(context);
            commandService.AddCommand(_projectUpdateCommand.InnerCommand);
        }

        #endregion
    }
}
