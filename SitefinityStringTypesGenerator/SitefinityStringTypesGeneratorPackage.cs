using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using VSLangProj;
using System.Linq;
using System.IO;

namespace Telerik.SitefinityStringTypesGenerator
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidSitefinityStringTypesGeneratorPkgString)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string)]
    public sealed class SitefinityStringTypesGeneratorPackage : Package, IDisposable
    {
        private DTE dte;
        private SolutionEvents solutionEvents;
        private ProjectItemsEvents projectItemsEvents;
        FileSystemWatcher watcher;

        private string[] requiredFileNames;

        private bool watching;

        public SitefinityStringTypesGeneratorPackage()
        {
            this.watching = false;
            this.requiredFileNames = new string[] { "DynamicModulesConfig.config", "SitefinityDynamicTypes.tt" };
        }

        protected override void Initialize()
        {
            this.dte = GetService(typeof(SDTE)) as DTE;

            this.solutionEvents = this.dte.Events.SolutionEvents;
            this.solutionEvents.Opened += this.solutionEvents_Opened;
            this.solutionEvents.BeforeClosing += this.solutionEvents_BeforeClosing;

            this.projectItemsEvents = ((EnvDTE80.Events2)this.dte.Events).ProjectItemsEvents;
            this.projectItemsEvents.ItemAdded += this.projectItemsEvents_ItemAdded;
            this.projectItemsEvents.ItemRemoved += this.projectItemsEvents_ItemRemoved;
        }

        private void solutionEvents_BeforeClosing()
        {
            this.TryStopWatching();
        }

        private void solutionEvents_Opened()
        {
            this.TryStartWatching();
        }

        private void projectItemsEvents_ItemAdded(ProjectItem projectItem)
        {
            string currentFileName = projectItem.Name;
            if (!string.IsNullOrEmpty(currentFileName) && this.requiredFileNames.Contains(currentFileName, StringComparer.InvariantCultureIgnoreCase))
            {
                this.TryStartWatching();
            }
        }

        private void projectItemsEvents_ItemRemoved(ProjectItem projectItem)
        {
            string currentFileName = projectItem.Name;
            if (!string.IsNullOrEmpty(currentFileName) && this.requiredFileNames.Contains(currentFileName, StringComparer.InvariantCultureIgnoreCase))
            {
                this.TryStopWatching();
            }
        }

        private void TryStartWatching()
        {
            if (!this.watching)
            {
                try
                {
                    ProjectItem configProjectItem = this.dte.Solution.FindProjectItem(this.requiredFileNames[0]);
                    ProjectItem templateProjectItem = this.dte.Solution.FindProjectItem(this.requiredFileNames[1]);
                    if (configProjectItem != null && templateProjectItem != null)
                    {
                        this.watching = true;
                        this.DisposeWatcher();
                        this.watcher = new FileSystemWatcher();

                        string fullConfigPath = configProjectItem.FileNames[0];
                        string configFolder = Path.GetDirectoryName(fullConfigPath);

                        watcher.Path = configFolder;
                        watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
                        watcher.Filter = configProjectItem.Name;

                        watcher.Changed += this.watcher_Changed;
                        watcher.EnableRaisingEvents = true;
                    }
                }
                catch
                {
                    this.watching = false;
                }
            }
        }

        private void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            ProjectItem templateProjectItem = this.dte.Solution.FindProjectItem(this.requiredFileNames[1]);
            if (templateProjectItem != null)
            {
                VSProjectItem vsItem = templateProjectItem.Object as VSProjectItem;
                if (vsItem != null)
                {
                    vsItem.RunCustomTool();
                }
            }
            else
            {
                this.watching = true;
                this.TryStopWatching();
            }
        }

        private void TryStopWatching()
        {
            if (this.watching)
            {
                try
                {
                    this.watching = false;
                    this.DisposeWatcher();
                }
                catch
                {
                    this.watching = false;
                }
            }
        }

        private void DisposeWatcher()
        {
            if (this.watcher != null)
            {
                this.watcher.EnableRaisingEvents = false;
                this.watcher.Changed -= this.watcher_Changed;
                this.watcher.Dispose();
                this.watcher = null;
            }
        }

        protected override int QueryClose(out bool canClose)
        {
            int result = base.QueryClose(out canClose);
            if (!canClose)
            {
                return result;
            }

            this.TryStopWatching();

            if (this.solutionEvents != null)
            {
                this.solutionEvents.Opened -= this.solutionEvents_Opened;
                this.solutionEvents.BeforeClosing -= this.solutionEvents_BeforeClosing;
                this.solutionEvents = null;
            }

            if (this.projectItemsEvents != null)
            {
                this.projectItemsEvents.ItemAdded -= this.projectItemsEvents_ItemAdded;
                this.projectItemsEvents.ItemRemoved -= this.projectItemsEvents_ItemRemoved;
                this.projectItemsEvents = null;
            }

            return result;
        }

        public void Dispose()
        {
            this.DisposeWatcher();
        }
    }
}
