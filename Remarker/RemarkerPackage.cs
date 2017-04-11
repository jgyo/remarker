namespace YoderZone.Extensions.Remarker
{
    using Microsoft.VisualStudio.Shell;
    using System;
    using System.ComponentModel.Design;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using YoderZone.Extensions.Remarker.Options;
    using YoderZone.Extensions.Remarker.Service;

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
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]

    //+ This attribute registers an Options page.
    [ProvideOptionPage(typeof(RemarkerGeneralOptionsPage), "Remarker", "Comment Settings", 110, 0,
        true)]

    //+ This attribute registers an Options page.
    [ProvideOptionPage(typeof(RemarkerTaskOptionsPage), "Remarker", "Task Settings", 110, 0, true)]

    //+ Used to implement a VSPackage's Visual Studio settings
    //+ support
    [ProvideProfile(typeof(ProfileManager), "Remarker", "Settings", 110, 0, false)]

    //+ This attribute indicates that the package proffers a service
    [ProvideService(typeof(IRemarkerService))]
    [Guid(Guids.guidRemarkerPkgString)]
    public sealed class RemarkerPackage : Package, IDisposable
    {
        // Default version: 1.4
        public const int DEFAULT_VERSION = 0x0104;

        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly int _version = RemarkerPackage.GetCurrentVersion();

        private bool _disposedValue = false;
        private RemarkerService _service;

        static RemarkerPackage()
        {
        }

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require
        /// any Visual Studio service because at this point the package object is created but
        /// not sited yet inside Visual Studio environment. The place to do all the other
        /// initialization is the Initialize method.
        /// </summary>
        public RemarkerPackage()
        {
            try
            {
                var container = (IServiceContainer)this;
                var callback = new ServiceCreatorCallback(this.CreateService);
                container.AddService(typeof(IRemarkerService), callback, true);
            }
            // ReSharper disable once RedundantCatchClause
            // ReSharper disable once UnusedVariable
            catch (Exception)
            {
                throw;
            }
        }

        public static int Version => RemarkerPackage._version;

        // This code added to correctly implement the disposable pattern.
        public void Dispose() =>
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!this._disposedValue)
            {
                if (disposing)
                {
                    this._service.Dispose();
                }

                this._disposedValue = true;
            }
        }

        /// <summary>
        /// Called when the VSPackage is loaded by Visual Studio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            this._service = Package.GetGlobalService(typeof(IRemarkerService)) as RemarkerService;
        }

        private static int GetCurrentVersion()
        {
            // Extract the version
            var assembly = typeof(RemarkerPackage).Assembly;
            int result = GetCurrentVersion(assembly);
            return result == 0 ? RemarkerPackage.DEFAULT_VERSION : result;
        }

        private static int GetCurrentVersion(Assembly assembly)
        {
            string[] strings = assembly.FullName.Split(',');
            foreach (var s in strings)
            {
                if (s.StartsWith("Version"))
                {
                    string[] split = s.Split('=');
                    string s1 = split[1];
                    string[] split1 = s1.Split('.');
                    var result = int.Parse(split1[0]);
                    result <<= 8;
                    result += int.Parse(split1[1]);
                    return result;
                }
            }

            return 0;
        }

        private object CreateService(IServiceContainer container, Type servicetype)
        {
            if (typeof(IRemarkerService) != servicetype)
            {
                return null;
            }

            if (this._service != null)
            {
                return this._service;
            }

            this._service = new RemarkerService(this);

            return this._service;
        }

        // To detect redundant calls
    }
}