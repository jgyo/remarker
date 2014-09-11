// ***********************************************************************
// Assembly         : Options.Package
// Solution         : YoderZone.Com.Extensions
// File name        : RemarkerTaskOptionsPage.cs
// Author           : Gil Yoder
// Created          : 09 03,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 07, 2014
// ***********************************************************************

namespace YoderZone.Extensions.OptionsPackage.Options
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
public class RemarkerTaskOptionsPage : DialogPage
{
    #region Fields

    /// <summary>
    ///     Options for controlling the operation.
    /// </summary>
    private readonly RemarkerSettings settings = RemarkerSettings.Default;

    private TaskOptionsPage control;

    private TaskOptions model;

    #endregion

    public TaskOptionsPage Control
    {
        get
        {
            return this.control ?? (control = TaskOptionsPage.DefaultTaskOptionsPage);
        }
    }

    public TaskOptions Model
    {
        get
        {
            return this.model ?? (model = TaskOptions.DefaultOptions);
        }
    }


    /// <summary>
    /// Handles Deactive messages from the Visual Studio environment.
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
    /// Handles Windows Activate messages from the Visual Studio environment.
    /// </summary>
    /// <param name="e">[in] Arguments to event handler.</param>
    protected override void OnActivate(CancelEventArgs e)
    {
        if (this.control != null)
        { this.SetValues(); }
        base.OnActivate(e);
    }

    private void SetValues()
    {
        var task = new TaskAttributes
        {
            Name = this.settings.Task01,
            IsBold = this.settings.TaskBold01,
            Color =
            this.settings.TaskColor01.ConvertStringToWpfColor(),
            Typeface = this.settings.TaskTypeface01
        };
        var tasks = new List<TaskAttributes>(10);
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.settings.Task02,
            IsBold = this.settings.TaskBold02,
            Color = this.settings.TaskColor02.ConvertStringToWpfColor(),
            Typeface = this.settings.TaskTypeface02
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.settings.Task03,
            IsBold = this.settings.TaskBold03,
            Color = this.settings.TaskColor03.ConvertStringToWpfColor(),
            Typeface = this.settings.TaskTypeface03
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.settings.Task04,
            IsBold = this.settings.TaskBold04,
            Color = this.settings.TaskColor04.ConvertStringToWpfColor(),
            Typeface = this.settings.TaskTypeface04
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.settings.Task05,
            IsBold = this.settings.TaskBold05,
            Color = this.settings.TaskColor05.ConvertStringToWpfColor(),
            Typeface = this.settings.TaskTypeface05
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.settings.Task06,
            IsBold = this.settings.TaskBold06,
            Color = this.settings.TaskColor06.ConvertStringToWpfColor(),
            Typeface = this.settings.TaskTypeface06
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.settings.Task07,
            IsBold = this.settings.TaskBold07,
            Color = this.settings.TaskColor07.ConvertStringToWpfColor(),
            Typeface = this.settings.TaskTypeface07
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.settings.Task08,
            IsBold = this.settings.TaskBold08,
            Color = this.settings.TaskColor08.ConvertStringToWpfColor(),
            Typeface = this.settings.TaskTypeface08
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.settings.Task09,
            IsBold = this.settings.TaskBold09,
            Color = this.settings.TaskColor09.ConvertStringToWpfColor(),
            Typeface = this.settings.TaskTypeface09
        };
        tasks.Add(task);

        task = new TaskAttributes
        {
            Name = this.settings.Task10,
            IsBold = this.settings.TaskBold10,
            Color = this.settings.TaskColor10.ConvertStringToWpfColor(),
            Typeface = this.settings.TaskTypeface10
        };
        tasks.Add(task);

        this.Model.LoadTasks(tasks);
    }

    #region Public Properties

    [Category("Task Settings")]
    [DisplayName("Dummy Property")]
    public string DummyProperty { get; set; }

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
            return this.Control;
        }
    }

    #endregion

    #region Public Methods and Operators



    /// <summary>
    ///     Reset settings to their default values.
    /// </summary>
    /// <seealso cref="M:Microsoft.VisualStudio.Shell.DialogPage.ResetSettings()" />
    public override void ResetSettings()
    {
        PropertyDescriptorCollection remarkerSettingsPropertyDescriptors =
            TypeDescriptor.GetProperties(typeof(RemarkerSettings));

        // ReSharper disable PossibleNullReferenceException
        PropertyDescriptor rpd;
        DefaultValueAttribute dva;

        // ReSharper disable PossibleNullReferenceException

        rpd = remarkerSettingsPropertyDescriptors["Task01"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.Task01 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskBold01"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskBold01 = (bool)dva.Value;

        rpd = remarkerSettingsPropertyDescriptors["TaskColor01"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskColor01 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskTypeface01"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskTypeface01 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["Task02"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.Task02 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskBold02"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskBold02 = (bool)dva.Value;

        rpd = remarkerSettingsPropertyDescriptors["TaskColor02"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskColor02 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskTypeface02"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskTypeface02 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["Task03"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.Task03 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskBold03"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskBold03 = (bool)dva.Value;

        rpd = remarkerSettingsPropertyDescriptors["TaskColor03"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskColor03 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskTypeface03"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskTypeface03 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["Task04"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.Task04 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskBold04"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskBold04 = (bool)dva.Value;

        rpd = remarkerSettingsPropertyDescriptors["TaskColor04"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskColor04 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskTypeface04"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskTypeface04 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["Task05"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.Task05 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskBold05"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskBold05 = (bool)dva.Value;

        rpd = remarkerSettingsPropertyDescriptors["TaskColor05"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskColor05 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskTypeface05"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskTypeface05 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["Task06"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.Task06 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskBold06"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskBold06 = (bool)dva.Value;

        rpd = remarkerSettingsPropertyDescriptors["TaskColor06"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskColor06 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskTypeface06"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskTypeface06 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["Task07"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.Task07 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskBold07"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskBold07 = (bool)dva.Value;

        rpd = remarkerSettingsPropertyDescriptors["TaskColor07"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskColor07 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskTypeface07"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskTypeface07 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["Task08"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.Task08 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskBold08"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskBold08 = (bool)dva.Value;

        rpd = remarkerSettingsPropertyDescriptors["TaskColor08"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskColor08 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskTypeface08"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskTypeface08 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["Task09"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.Task09 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskBold09"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskBold09 = (bool)dva.Value;

        rpd = remarkerSettingsPropertyDescriptors["TaskColor09"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskColor09 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskTypeface09"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskTypeface09 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["Task10"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.Task10 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskBold10"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskBold10 = (bool)dva.Value;

        rpd = remarkerSettingsPropertyDescriptors["TaskColor10"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskColor10 = dva.Value as string;

        rpd = remarkerSettingsPropertyDescriptors["TaskTypeface10"];
        dva = rpd.Attributes[typeof(DefaultValueAttribute)] as
              DefaultValueAttribute;
        this.settings.TaskTypeface10 = dva.Value as string;

        // ReSharper restore PossibleNullReferenceException

        this.settings.Save();
    }

    /// <summary>
    ///     Called by Visual Studio to store the settings of a dialog page in local
    ///     storage,
    ///     typically the registry.
    /// </summary>
    /// <seealso
    ///     cref="M:Microsoft.VisualStudio.Shell.DialogPage.SaveSettingsToStorage()" />
    public void ApplyChanges()
    {
        var task = this.Model.Tasks[0];
        this.settings.Task01 = task.Name;
        this.settings.TaskBold01 = task.IsBold;
        this.settings.TaskColor01 = task.Color.ConvertWpfColorToString();
        this.settings.TaskTypeface01 = task.Typeface;

        task = this.Model.Tasks[1];
        this.settings.Task02 = task.Name;
        this.settings.TaskBold02 = task.IsBold;
        this.settings.TaskColor02 = task.Color.ConvertWpfColorToString();
        this.settings.TaskTypeface02 = task.Typeface;

        task = this.Model.Tasks[2];
        this.settings.Task03 = task.Name;
        this.settings.TaskBold03 = task.IsBold;
        this.settings.TaskColor03 = task.Color.ConvertWpfColorToString();
        this.settings.TaskTypeface03 = task.Typeface;

        task = this.Model.Tasks[3];
        this.settings.Task04 = task.Name;
        this.settings.TaskBold04 = task.IsBold;
        this.settings.TaskColor04 = task.Color.ConvertWpfColorToString();
        this.settings.TaskTypeface04 = task.Typeface;

        task = this.Model.Tasks[4];
        this.settings.Task05 = task.Name;
        this.settings.TaskBold05 = task.IsBold;
        this.settings.TaskColor05 = task.Color.ConvertWpfColorToString();
        this.settings.TaskTypeface05 = task.Typeface;

        task = this.Model.Tasks[5];
        this.settings.Task06 = task.Name;
        this.settings.TaskBold06 = task.IsBold;
        this.settings.TaskColor06 = task.Color.ConvertWpfColorToString();
        this.settings.TaskTypeface06 = task.Typeface;

        task = this.Model.Tasks[6];
        this.settings.Task07 = task.Name;
        this.settings.TaskBold07 = task.IsBold;
        this.settings.TaskColor07 = task.Color.ConvertWpfColorToString();
        this.settings.TaskTypeface07 = task.Typeface;

        task = this.Model.Tasks[7];
        this.settings.Task08 = task.Name;
        this.settings.TaskBold08 = task.IsBold;
        this.settings.TaskColor08 = task.Color.ConvertWpfColorToString();
        this.settings.TaskTypeface08 = task.Typeface;

        task = this.Model.Tasks[8];
        this.settings.Task09 = task.Name;
        this.settings.TaskBold09 = task.IsBold;
        this.settings.TaskColor09 = task.Color.ConvertWpfColorToString();
        this.settings.TaskTypeface09 = task.Typeface;

        task = this.Model.Tasks[9];
        this.settings.Task10 = task.Name;
        this.settings.TaskBold10 = task.IsBold;
        this.settings.TaskColor10 = task.Color.ConvertWpfColorToString();
        this.settings.TaskTypeface10 = task.Typeface;

        this.settings.Save();
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
        if (this.control != null && control.IsDisposed == false)
        {
            control.Dispose();
        }

        base.Dispose(disposing);
    }

    #endregion
}
}