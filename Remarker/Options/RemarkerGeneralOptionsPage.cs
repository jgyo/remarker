// ***********************************************************************
// Assembly         : Options.Package
// Solution         : YoderZone.Com.Extensions
// File name        : RemarkerGeneralOptionsPage.cs
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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Microsoft.VisualStudio.Shell;

using YoderZone.Extensions.OptionsDialog;
using YoderZone.Extensions.OptionsDialog.ViewModel;
using YoderZone.Extensions.Remarker.Remarker.Service;

#endregion

/// <summary>
///     A remarker general options page.
/// </summary>
/// <seealso cref="T:Microsoft.VisualStudio.Shell.DialogPage" />
[ClassInterface(ClassInterfaceType.AutoDual)]
[ToolboxItem(false)]
[CLSCompliant(false)]
[ComVisible(true)]
public sealed class RemarkerGeneralOptionsPage : DialogPage
{
    // private readonly RemarkerSettings settings = RemarkerSettings.Default;

    #region Fields

    private readonly CommentOptions model;

    private readonly ProfileManager profileManager;

    private readonly RemarkerService service;

    private CommentOptionsPage control;

    private bool isActivated;

    private bool shouldSave;

    #endregion

    #region Constructors and Destructors

    public RemarkerGeneralOptionsPage()
    {
        this.profileManager = new ProfileManager();
        this.service = this.profileManager.Service;
        this.model = new CommentOptions();
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
            return this.control ?? (this.control = new CommentOptionsPage(
                this.model));
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
        CommentOptionsPage page = this.control;
        if (page != null && page.IsDisposed == false)
        {
            page.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    ///     Called by Visual Studio when an Options page is activated.
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
    ///     Called by Visual Studio when changes should be saved.
    /// </summary>
    /// <param name="e">[in] Arguments to event handler.</param>
    protected override void OnApply(PageApplyEventArgs e)
    {
        base.OnApply(e);
        this.shouldSave = true;
    }

    /// <summary>
    ///     Called by Visual Studio when closing Options.
    /// </summary>
    /// <param name="e">[in] Arguments to event handler.</param>
    protected override void OnClosed(EventArgs e)
    {
        if (!this.isActivated)
        {
            // noop
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
    ///     Called by Visual Studio when an Options page is deactivated.
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

    // Transfers settings from the model to the service.
    private void ApplyChanges()
    {
        this.service.GiganticFactor = this.model.Plus3;
        this.service.HugeFactor = this.model.Plus2;
        this.service.LargeFactor = this.model.Plus1;
        this.service.SmallFactor = this.model.Less1;
        this.service.TinyFactor = this.model.Less2;
        this.service.MicroFactor = this.model.Less3;
        this.service.FontFamily = this.model.SelectedFontFamilyName;
    }

    // Transfers settings from the service to the model.
    private void SetValues()
    {
        this.model.Plus3 = this.service.GiganticFactor;
        this.model.Plus2 = this.service.HugeFactor;
        this.model.Plus1 = this.service.LargeFactor;
        this.model.Less1 = this.service.SmallFactor;
        this.model.Less2 = this.service.TinyFactor;
        this.model.Less3 = this.service.MicroFactor;
        this.model.SelectedFontFamilyName = this.service.FontFamily;
    }

    #endregion
}
}