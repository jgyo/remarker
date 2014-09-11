namespace YoderZone.Extensions.OptionsPackage
{
using System.Diagnostics;
using System.Runtime.InteropServices;

using Microsoft.VisualStudio.Shell;

using YoderZone.Extensions.OptionsPackage.Options;

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
//+ This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
//+ a package.
[PackageRegistration(UseManagedResourcesOnly = true)]

//+ This attribute is used to register the information needed to show this package
//+ in the Help/About dialog of Visual Studio.
[InstalledProductRegistration("#110", "#112", "1.0",
                              IconResourceID = 400)]
[Guid(GuidList.guidRemarkerPkgString)]
[ProvideOptionPage(typeof(RemarkerGeneralOptionsPage), "Remarker",
                   "Comment Settings", 0, 0, true)]
[ProvideOptionPage(typeof(RemarkerTaskOptionsPage), "Remarker",
                   "Task Settings", 0, 0, true)]
public sealed class RemarkerPackage : Package
{
    /// <summary>
    /// Default constructor of the package.
    /// Inside this method you can place any initialization code that does not require
    /// any Visual Studio service because at this point the package object is created but
    /// not sited yet inside Visual Studio environment. The place to do all the other
    /// initialization is the Initialize method.
    /// </summary>
    public RemarkerPackage()
    {
    }



    /////////////////////////////////////////////////////////////////////////////
    // Overridden Package Implementation
    #region Package Members

    /// <summary>
    /// Initialization of the package; this method is called right after the package is sited, so this is the place
    /// where you can put all the initialization code that rely on services provided by VisualStudio.
    /// </summary>
    protected override void Initialize()
    {
        base.Initialize();

//!        var page = this.GetDialogPage(typeof(RemarkerGeneralOptionsPage)) as
//!                   RemarkerGeneralOptionsPage;
//!              OptionsSettings.Page = page;
    }
    #endregion

}
}
