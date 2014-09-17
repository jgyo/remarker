namespace YoderZone.Extensions.Remarker
{
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio.Shell;

using YoderZone.Extensions.OptionsPackage.Options;
using YoderZone.Extensions.OptionsPackage.Remarker.Service;
using YoderZone.Extensions.Remarker.Options;
using YoderZone.Extensions.Remarker.Remarker.Service;

/// <summary>
/// This is the class that implements the package exposed by this assembly.
///
/// The minimum requirement for a class to be considered a valid package for Visual Studio
/// is to implement the IVsPackage interface and register itself with the shell.
/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
/// to do it: it derives from the Package class that provides the implementation of the
/// IVsPackage interface and uses the registration attributes defined in the framework to
/// register itself and its components with the shell.
/// </summary>
//+ This attribute tells the PkgDef creation utility (CreatePkgDef.exe)
//+ that this class is a package.
[PackageRegistration(UseManagedResourcesOnly = true)]

//+ This attribute is used to register the information needed to
//+ show this package in the Help/About dialog of Visual Studio.
[InstalledProductRegistration("#110", "#112", "1.0",
                              IconResourceID = 400)]

//+ This attribute registers an Options page.
[ProvideOptionPage(typeof(RemarkerGeneralOptionsPage), "Remarker",
                   "Comment Settings", 110, 0, true)]

//+ This attribute registers an Options page.
[ProvideOptionPage(typeof(RemarkerTaskOptionsPage), "Remarker",
                   "Task Settings", 110, 0, true)]

//+ Used to implement a VSPackage's Visual Studio settings
//+ support
[ProvideProfile(typeof(ProfileManager), "Remarker", "Settings", 110, 0,
                false)]

//+ This attribute indicates that the package proffers a service
[ProvideService(typeof(IRemarkerService))]
[Guid(Guids.guidRemarkerPkgString)]
public sealed class RemarkerPackage : Package
{
    private static readonly int version = GetCurrentVersion();

    private RemarkerService service;

    // Default version: 1.2
    public const int DEFAULT_VERSION = 0x0102;

    /// <summary>
    /// Default constructor of the package.
    /// Inside this method you can place any initialization code that does not require
    /// any Visual Studio service because at this point the package object is created but
    /// not sited yet inside Visual Studio environment. The place to do all the other
    /// initialization is the Initialize method.
    /// </summary>
    public RemarkerPackage()
    {
        var container = (IServiceContainer)this;
        var callback = new ServiceCreatorCallback(this.CreateService);
        container.AddService(typeof(IRemarkerService), callback, true);
    }

    private object CreateService(IServiceContainer container,
                                 Type servicetype)
    {
        if (typeof(IRemarkerService) != servicetype)
        {
            return null;
        }

        Trace.WriteLine(
            string.Format("RemarkerPackage::CreateService - Creating service: {0}",
                          servicetype));

        if (this.service == null)
        {
            this.service = new RemarkerService(this);
        }

        return service;
    }

    public static int Version
    {
        get
        {
            return version;
        }
    }

    /////////////////////////////////////////////////////////////////////////////
    // Overridden Package Implementation
    #region Package Members

    //x private EnvDTE.WindowEvents windowEvents;

    //x TODO: Determine if this can be put to use.

    /*    /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so
        /// this is the place  where you can put all the initialization code that rely on services
        /// provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            // TODO: Determine if this is necessary
            var dte = this.GetService(typeof(EnvDTE.DTE)) as EnvDTE.DTE;
            if (dte != null)
            {
                this.windowEvents = dte.Events.WindowEvents;
                this.windowEvents.WindowActivated += this.windowEvents_WindowActivated;
                this.windowEvents.WindowClosing += this.windowEvents_WindowClosing;
            }

            (RemarkerService)this.GetService(typeof(IRemarkerService));
        }*/

    /*    // Used to disable or enable menu options as appropriate and other initialization
        // activities.
        // TODO: Determine if these can be deleted.
        void windowEvents_WindowClosing(EnvDTE.Window Window)
        {
        }

        void windowEvents_WindowActivated(EnvDTE.Window GotFocus,
                                          EnvDTE.Window LostFocus)
        {
        }*/

    #endregion

    private static int GetCurrentVersion()
    {
        // Extract the version
        var assembly = typeof(RemarkerPackage).Assembly;
        var attribs = assembly.GetCustomAttributes(typeof(
                          System.Reflection.AssemblyFileVersionAttribute), false);
        if (attribs.Length == 0) { return DEFAULT_VERSION; }

        var attrib = (System.Reflection.AssemblyFileVersionAttribute)attribs[0];

        try
        {
            // Starting with an accumulator value of zero do the following for each number segment:
            // shift the accumulator left 8 bits and or the accumulator with the value of the number.
            int extractedVersion = attrib.Version.Split('.')
                                   .Select(int.Parse)
                                   .Take(2)                        //! We only want major and minor versions.
                                   .Aggregate(0, (acc, i) => acc << 8 | i);
            Trace.TraceInformation("RemarkerPackage.CURRENT_VERSION == {0:X}",
                                   extractedVersion);
            return extractedVersion;
        }
        catch (Exception ex)
        {
            Trace.TraceError("IndentGuide::GetCurrentVersion: {0}", ex);
            return DEFAULT_VERSION;
        }
    }

}
}
