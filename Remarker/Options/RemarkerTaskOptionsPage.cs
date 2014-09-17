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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Microsoft.VisualStudio.Shell;

using YoderZone.Extensions.OptionsDialog;
using YoderZone.Extensions.OptionsDialog.ViewModel;
using YoderZone.Extensions.OptionsPackage.Remarker.Utilities;
using YoderZone.Extensions.Remarker.Remarker.Service;

using TaskOptions =
    YoderZone.Extensions.OptionsDialog.ViewModel.TaskOptions;

#endregion

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

    private readonly TaskOptions model;

    private readonly ProfileManager profileManager;

    /// <summary>
    ///     Options for controlling the operation.
    /// </summary>
    //private readonly RemarkerSettings service = RemarkerSettings.Default;
    private readonly RemarkerService service;

    private TaskOptionsPage control;

    private bool isActivated;

    private bool shouldSave;

    #endregion

    #region Constructors and Destructors

    public RemarkerTaskOptionsPage()
    {
        this.profileManager = new ProfileManager();
        this.service = this.profileManager.Service;
        this.model = new TaskOptions();
        this.isActivated = false;
    }

    #endregion

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
    protected override IWin32Window Window
    {
        get
        {
            return this.control ?? (this.control = new TaskOptionsPage(this.model));
        }
    }

    #endregion

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
        if (this.control != null && this.control.IsDisposed == false)
        {
            this.control.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    ///     Handles Windows Activate messages from the Visual Studio environment.
    /// </summary>
    /// <param name="e">[in] Arguments to event handler.</param>
    protected override void OnActivate(CancelEventArgs e)
    {
        if (!this.isActivated)
        {
            this.profileManager.ProtectSettings();
            this.isActivated = true;
            this.shouldSave = false;
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
        this.shouldSave = true;
    }

    /// <summary>
    /// Handles Close messages from the Visual Studio environment.
    /// </summary>
    /// <param name="e">[in] Arguments to event handler.</param>
    protected override void OnClosed(EventArgs e)
    {
        if (!this.isActivated)
        {
            // no op
        }
        else if (this.shouldSave)
        {
            this.profileManager.CommitSettings();
        }
        else
        {
            this.profileManager.RollBackSettings();
        }

        this.isActivated = false;
        base.OnClosed(e);
    }

    /// <summary>
    ///     Handles Deactivate messages from the Visual Studio environment.
    /// </summary>
    /// <param name="e">[in] Arguments to event handler.</param>
    protected override void OnDeactivate(CancelEventArgs e)
    {
        base.OnDeactivate(e);
        if (e.Cancel || this.control == null)
        {
            return;
        }

        e.Cancel = !this.control.CanDeactivate();
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
        TaskAttributes task = this.model.Tasks[0];
        this.service.Task01 = task.Name;
        this.service.TaskBold01 = task.IsBold;
        this.service.TaskColor01 = task.Color.ConvertWpfColorToString();
        this.service.TaskTypeface01 = task.Typeface;

        task = this.model.Tasks[1];
        this.service.Task02 = task.Name;
        this.service.TaskBold02 = task.IsBold;
        this.service.TaskColor02 = task.Color.ConvertWpfColorToString();
        this.service.TaskTypeface02 = task.Typeface;

        task = this.model.Tasks[2];
        this.service.Task03 = task.Name;
        this.service.TaskBold03 = task.IsBold;
        this.service.TaskColor03 = task.Color.ConvertWpfColorToString();
        this.service.TaskTypeface03 = task.Typeface;

        task = this.model.Tasks[3];
        this.service.Task04 = task.Name;
        this.service.TaskBold04 = task.IsBold;
        this.service.TaskColor04 = task.Color.ConvertWpfColorToString();
        this.service.TaskTypeface04 = task.Typeface;

        task = this.model.Tasks[4];
        this.service.Task05 = task.Name;
        this.service.TaskBold05 = task.IsBold;
        this.service.TaskColor05 = task.Color.ConvertWpfColorToString();
        this.service.TaskTypeface05 = task.Typeface;

        task = this.model.Tasks[5];
        this.service.Task06 = task.Name;
        this.service.TaskBold06 = task.IsBold;
        this.service.TaskColor06 = task.Color.ConvertWpfColorToString();
        this.service.TaskTypeface06 = task.Typeface;

        task = this.model.Tasks[6];
        this.service.Task07 = task.Name;
        this.service.TaskBold07 = task.IsBold;
        this.service.TaskColor07 = task.Color.ConvertWpfColorToString();
        this.service.TaskTypeface07 = task.Typeface;

        task = this.model.Tasks[7];
        this.service.Task08 = task.Name;
        this.service.TaskBold08 = task.IsBold;
        this.service.TaskColor08 = task.Color.ConvertWpfColorToString();
        this.service.TaskTypeface08 = task.Typeface;

        task = this.model.Tasks[8];
        this.service.Task09 = task.Name;
        this.service.TaskBold09 = task.IsBold;
        this.service.TaskColor09 = task.Color.ConvertWpfColorToString();
        this.service.TaskTypeface09 = task.Typeface;

        task = this.model.Tasks[9];
        this.service.Task10 = task.Name;
        this.service.TaskBold10 = task.IsBold;
        this.service.TaskColor10 = task.Color.ConvertWpfColorToString();
        this.service.TaskTypeface10 = task.Typeface;
    }

    private void SetValues()
    {
        var task = new TaskAttributes
        {
            Name = this.service.Task01,
            IsBold = this.service.TaskBold01,
            Color =
            this.service.TaskColor01.ConvertStringToWpfColor(),
            Typeface = this.service.TaskTypeface01
        };

        // ReSharper disable once UseObjectOrCollectionInitializer
        var tasks = new List<TaskAttributes>(10);
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.service.Task02,
            IsBold = this.service.TaskBold02,
            Color = this.service.TaskColor02.ConvertStringToWpfColor(),
            Typeface = this.service.TaskTypeface02
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.service.Task03,
            IsBold = this.service.TaskBold03,
            Color = this.service.TaskColor03.ConvertStringToWpfColor(),
            Typeface = this.service.TaskTypeface03
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.service.Task04,
            IsBold = this.service.TaskBold04,
            Color = this.service.TaskColor04.ConvertStringToWpfColor(),
            Typeface = this.service.TaskTypeface04
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.service.Task05,
            IsBold = this.service.TaskBold05,
            Color = this.service.TaskColor05.ConvertStringToWpfColor(),
            Typeface = this.service.TaskTypeface05
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.service.Task06,
            IsBold = this.service.TaskBold06,
            Color = this.service.TaskColor06.ConvertStringToWpfColor(),
            Typeface = this.service.TaskTypeface06
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.service.Task07,
            IsBold = this.service.TaskBold07,
            Color = this.service.TaskColor07.ConvertStringToWpfColor(),
            Typeface = this.service.TaskTypeface07
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.service.Task08,
            IsBold = this.service.TaskBold08,
            Color = this.service.TaskColor08.ConvertStringToWpfColor(),
            Typeface = this.service.TaskTypeface08
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.service.Task09,
            IsBold = this.service.TaskBold09,
            Color = this.service.TaskColor09.ConvertStringToWpfColor(),
            Typeface = this.service.TaskTypeface09
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.service.Task10,
            IsBold = this.service.TaskBold10,
            Color = this.service.TaskColor10.ConvertStringToWpfColor(),
            Typeface = this.service.TaskTypeface10
        };
        tasks.Add(task);

        this.model.LoadTasks(tasks);
    }

    #endregion
}
}