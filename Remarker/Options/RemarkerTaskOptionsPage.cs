// ***********************************************************************
// Assembly         : Options.Package
// Solution         : YoderZone.Com.Extensions
// File name        : RemarkerTaskOptionsPage.cs
// Author           : Gil Yoder
// Created          : 09 11,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 16, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Remarker.Options
{
    #region Imports

    using Microsoft.VisualStudio.Shell;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using YoderZone.Extensions.Options;
    using YoderZone.Extensions.Options.ViewModel;
    using YoderZone.Extensions.Remarker.Service;
    using YoderZone.Extensions.Remarker.Utilities;

    #endregion Imports

    /// <summary>
    ///     A remarker general options page.
    /// </summary>
    /// <seealso cref="T:Microsoft.VisualStudio.Shell.DialogPage" />
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ToolboxItem(false)]
    [CLSCompliant(false)]
    [ComVisible(true)]
    public sealed class RemarkerTaskOptionsPage : DialogPage
    {
        #region Fields

        private readonly TaskOptions _model;

        private readonly ProfileManager _profileManager;

        /// <summary>
        ///     Options for controlling the operation.
        /// </summary>
        //private readonly RemarkerSettings service = RemarkerSettings.Default;
        private readonly RemarkerService _service;

        private TaskOptionsPage _control;

        private bool _isActivated;

        private bool _shouldSave;

        #endregion Fields

        #region Constructors and Destructors

        public RemarkerTaskOptionsPage()
        {
            this._profileManager = new ProfileManager();
            this._service = this._profileManager.Service;
            this._model = new TaskOptions();
            this._isActivated = false;
        }

        #endregion Constructors and Destructors

        #region Properties

        /// <summary>
        ///     Gets the window that is used as the user interface of the dialog page.
        /// </summary>
        /// <value>
        ///     An <see cref="T:System.Windows.Forms.IWin32Window" /> that provides the
        ///     handle to the
        ///     window that acts as the user interface for the dialog page.
        /// </value>
        /// <seealso cref="P:Microsoft.VisualStudio.Shell.DialogPage.Window" />
        [Browsable(false)]
        protected override IWin32Window Window => this._control ?? (this._control = new TaskOptionsPage(this._model));

        #endregion Properties

        #region Methods

        /// <summary>
        ///     Releases the unmanaged resources that are used by a dialog page class and
        ///     optionally
        ///     releases the managed resources; the parent class,
        ///     <see cref="T:System.ComponentModel.Component" /> supports unmanaged
        ///     resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only
        ///     unmanaged
        ///     resources.
        /// </param>
        /// <seealso cref="M:Microsoft.VisualStudio.Shell.DialogPage.Dispose(bool)" />
        protected override void Dispose(bool disposing)
        {
            if (this._control != null && this._control.IsDisposed == false)
            {
                this._control.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        ///     Handles Windows Activate messages from the Visual Studio environment.
        /// </summary>
        /// <param name="e">[in] Arguments to event handler.</param>
        protected override void OnActivate(CancelEventArgs e)
        {
            if (!this._isActivated)
            {
                this._profileManager.ProtectSettings();
                this._isActivated = true;
                this._shouldSave = false;
                this.SetValues();
            }

            base.OnActivate(e);
        }

        /// <summary>
        /// Handles Apply messages from the Visual Studio environment.
        /// </summary>
        /// <param name="e">[in] Arguments to event handler.</param>
        protected override void OnApply(PageApplyEventArgs e)
        {
            base.OnApply(e);
            this._shouldSave = true;
        }

        /// <summary>
        /// Handles Close messages from the Visual Studio environment.
        /// </summary>
        /// <param name="e">[in] Arguments to event handler.</param>
        protected override void OnClosed(EventArgs e)
        {
            if (!this._isActivated)
            {
                // no op
            }
            else if (this._shouldSave)
            {
                this._profileManager.CommitSettings();
            }
            else
            {
                this._profileManager.RollBackSettings();
            }

            this._isActivated = false;
            base.OnClosed(e);
        }

        /// <summary>
        ///     Handles Deactivate messages from the Visual Studio environment.
        /// </summary>
        /// <param name="e">[in] Arguments to event handler.</param>
        protected override void OnDeactivate(CancelEventArgs e)
        {
            base.OnDeactivate(e);
            if (e.Cancel || this._control == null)
            {
                return;
            }

            e.Cancel = !this._control.CanDeactivate();
            if (e.Cancel)
            {
                return;
            }

            this.ApplyChanges();
        }

        /// <summary>
        ///     Called by Visual Studio to store the settings of a dialog page in local
        ///     storage,
        ///     typically the registry.
        /// </summary>
        /// <seealso
        ///     cref="M:Microsoft.VisualStudio.Shell.DialogPage.SaveSettingsToStorage()" />
        private void ApplyChanges()
        {
            TaskAttributes task = this._model.Tasks[0];
            this._service.Task01 = task.Name;
            this._service.TaskBold01 = task.IsBold;
            this._service.TaskColor01 = task.Color.ConvertWpfColorToString();
            this._service.TaskTypeface01 = task.Typeface;

            task = this._model.Tasks[1];
            this._service.Task02 = task.Name;
            this._service.TaskBold02 = task.IsBold;
            this._service.TaskColor02 = task.Color.ConvertWpfColorToString();
            this._service.TaskTypeface02 = task.Typeface;

            task = this._model.Tasks[2];
            this._service.Task03 = task.Name;
            this._service.TaskBold03 = task.IsBold;
            this._service.TaskColor03 = task.Color.ConvertWpfColorToString();
            this._service.TaskTypeface03 = task.Typeface;

            task = this._model.Tasks[3];
            this._service.Task04 = task.Name;
            this._service.TaskBold04 = task.IsBold;
            this._service.TaskColor04 = task.Color.ConvertWpfColorToString();
            this._service.TaskTypeface04 = task.Typeface;

            task = this._model.Tasks[4];
            this._service.Task05 = task.Name;
            this._service.TaskBold05 = task.IsBold;
            this._service.TaskColor05 = task.Color.ConvertWpfColorToString();
            this._service.TaskTypeface05 = task.Typeface;

            task = this._model.Tasks[5];
            this._service.Task06 = task.Name;
            this._service.TaskBold06 = task.IsBold;
            this._service.TaskColor06 = task.Color.ConvertWpfColorToString();
            this._service.TaskTypeface06 = task.Typeface;

            task = this._model.Tasks[6];
            this._service.Task07 = task.Name;
            this._service.TaskBold07 = task.IsBold;
            this._service.TaskColor07 = task.Color.ConvertWpfColorToString();
            this._service.TaskTypeface07 = task.Typeface;

            task = this._model.Tasks[7];
            this._service.Task08 = task.Name;
            this._service.TaskBold08 = task.IsBold;
            this._service.TaskColor08 = task.Color.ConvertWpfColorToString();
            this._service.TaskTypeface08 = task.Typeface;

            task = this._model.Tasks[8];
            this._service.Task09 = task.Name;
            this._service.TaskBold09 = task.IsBold;
            this._service.TaskColor09 = task.Color.ConvertWpfColorToString();
            this._service.TaskTypeface09 = task.Typeface;

            task = this._model.Tasks[9];
            this._service.Task10 = task.Name;
            this._service.TaskBold10 = task.IsBold;
            this._service.TaskColor10 = task.Color.ConvertWpfColorToString();
            this._service.TaskTypeface10 = task.Typeface;
        }

        private void SetValues()
        {
            var task = new TaskAttributes
            {
                Name = this._service.Task01,
                IsBold = this._service.TaskBold01,
                Color =
                this._service.TaskColor01.ConvertStringToWpfColor(),
                Typeface = this._service.TaskTypeface01
            };

            // ReSharper disable once UseObjectOrCollectionInitializer
            var tasks = new List<TaskAttributes>(10)
            {
                task
            };

            task = new TaskAttributes
            {
                Name = this._service.Task02,
                IsBold = this._service.TaskBold02,
                Color = this._service.TaskColor02.ConvertStringToWpfColor(),
                Typeface = this._service.TaskTypeface02
            };
            tasks.Add(task);

            task = new TaskAttributes
            {
                Name = this._service.Task03,
                IsBold = this._service.TaskBold03,
                Color = this._service.TaskColor03.ConvertStringToWpfColor(),
                Typeface = this._service.TaskTypeface03
            };
            tasks.Add(task);

            task = new TaskAttributes
            {
                Name = this._service.Task04,
                IsBold = this._service.TaskBold04,
                Color = this._service.TaskColor04.ConvertStringToWpfColor(),
                Typeface = this._service.TaskTypeface04
            };
            tasks.Add(task);

            task = new TaskAttributes
            {
                Name = this._service.Task05,
                IsBold = this._service.TaskBold05,
                Color = this._service.TaskColor05.ConvertStringToWpfColor(),
                Typeface = this._service.TaskTypeface05
            };
            tasks.Add(task);

            task = new TaskAttributes
            {
                Name = this._service.Task06,
                IsBold = this._service.TaskBold06,
                Color = this._service.TaskColor06.ConvertStringToWpfColor(),
                Typeface = this._service.TaskTypeface06
            };
            tasks.Add(task);

            task = new TaskAttributes
            {
                Name = this._service.Task07,
                IsBold = this._service.TaskBold07,
                Color = this._service.TaskColor07.ConvertStringToWpfColor(),
                Typeface = this._service.TaskTypeface07
            };
            tasks.Add(task);

            task = new TaskAttributes
            {
                Name = this._service.Task08,
                IsBold = this._service.TaskBold08,
                Color = this._service.TaskColor08.ConvertStringToWpfColor(),
                Typeface = this._service.TaskTypeface08
            };
            tasks.Add(task);

            task = new TaskAttributes
            {
                Name = this._service.Task09,
                IsBold = this._service.TaskBold09,
                Color = this._service.TaskColor09.ConvertStringToWpfColor(),
                Typeface = this._service.TaskTypeface09
            };
            tasks.Add(task);

            task = new TaskAttributes
            {
                Name = this._service.Task10,
                IsBold = this._service.TaskBold10,
                Color = this._service.TaskColor10.ConvertStringToWpfColor(),
                Typeface = this._service.TaskTypeface10
            };
            tasks.Add(task);

            this._model.LoadTasks(tasks);
        }

        #endregion Methods
    }
}