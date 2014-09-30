// <copyright file="RemarkerGeneralOptionsPage.cs" company="YoderZone.com">
// Copyright (c) 2014 Gil Yoder. All rights reserved.
// </copyright>
// <author>Gil Yoder</author>
// <date>9/27/2014</date>
// <summary>Implements the remarker general options page class</summary>
// <remarks>
// Licensed under the Microsoft Public License (Ms-PL); you may not
// use this file except in compliance with the License. You may obtain a copy
// of the License at
//
// https://remarker.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.
// </remarks>

namespace YoderZone.Extensions.Remarker.Options
{
#region Imports

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using global::NLog;

using Microsoft.VisualStudio.Shell;

using YoderZone.Extensions.NLog;
using YoderZone.Extensions.Options;
using YoderZone.Extensions.Options.ViewModel;
using YoderZone.Extensions.Remarker.Service;

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
    #region Static Fields

    /// <summary>
    ///     The logger.
    /// </summary>
    private static readonly Logger logger = SettingsHelper.CreateLogger();

    #endregion

    // private readonly RemarkerSettings settings = RemarkerSettings.Default;

    #region Fields

    /// <summary>
    ///     The model.
    /// </summary>
    private readonly CommentOptions model;

    /// <summary>
    ///     Manager for profile.
    /// </summary>
    private readonly ProfileManager profileManager;

    /// <summary>
    ///     The service.
    /// </summary>
    private readonly RemarkerService service;

    /// <summary>
    ///     The control.
    /// </summary>
    private CommentOptionsPage control;

    /// <summary>
    ///     true if this
    ///     YoderZone.Extensions.Remarker.Options.RemarkerGeneralOptionsPage is
    ///     activated.
    /// </summary>
    private bool isActivated;

    /// <summary>
    ///     true if should save.
    /// </summary>
    private bool shouldSave;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the
    ///     YoderZone.Extensions.Remarker.Options.RemarkerGeneralOptionsPage
    ///     class.
    /// </summary>
    public RemarkerGeneralOptionsPage()
    {
        logger.Debug("Entered constructor.");

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
    /// <seealso cref="P:Microsoft.VisualStudio.Shell.DialogPage.Window" />
    /// ###
    /// <value>
    ///     An <see cref="T:System.Windows.Forms.IWin32Window" /> that provides
    ///     the handle to the window that acts as the user interface for the
    ///     dialog page.
    /// </value>
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
    ///     Releases the unmanaged resources that are used by a dialog page class
    ///     and optionally releases the managed resources; the parent class,
    ///     <see cref="T:System.ComponentModel.Component" /> supports unmanaged
    ///     resources.
    /// </summary>
    /// <seealso cref="M:Microsoft.VisualStudio.Shell.DialogPage.Dispose(bool)" />
    /// ###
    /// <param name="disposing">
    ///     true to release both managed and unmanaged resources; false to
    ///     release only unmanaged resources.
    /// </param>
    protected override void Dispose(bool disposing)
    {
        logger.Debug("Entered method.");

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
    /// <seealso
    ///     cref="M:Microsoft.VisualStudio.Shell.DialogPage.OnActivate(CancelEventArgs)" />
    /// ###
    /// <param name="e">
    ///     [in] Arguments to event handler.
    /// </param>
    protected override void OnActivate(CancelEventArgs e)
    {
        logger.Debug("Entered method.");

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
    /// <seealso
    ///     cref="M:Microsoft.VisualStudio.Shell.DialogPage.OnApply(PageApplyEventArgs)" />
    /// ###
    /// <param name="e">
    ///     [in] Arguments to event handler.
    /// </param>
    protected override void OnApply(PageApplyEventArgs e)
    {
        logger.Debug("Entered method.");

        base.OnApply(e);
        this.shouldSave = true;
    }

    /// <summary>
    ///     Called by Visual Studio when closing Options.
    /// </summary>
    /// <seealso cref="M:Microsoft.VisualStudio.Shell.DialogPage.OnClosed(EventArgs)" />
    /// ###
    /// <param name="e">
    ///     [in] Arguments to event handler.
    /// </param>
    protected override void OnClosed(EventArgs e)
    {
        logger.Debug("Entered method.");

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
    /// <seealso
    ///     cref="M:Microsoft.VisualStudio.Shell.DialogPage.OnDeactivate(CancelEventArgs)" />
    /// ###
    /// <param name="e">
    ///     [in] Arguments to event handler.
    /// </param>
    protected override void OnDeactivate(CancelEventArgs e)
    {
        logger.Debug("Entered method.");

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
    ///     Transfers settings from the model to the service.
    /// </summary>
    private void ApplyChanges()
    {
        logger.Debug("Entered method.");

        this.service.GiganticFactor = this.model.Plus3;
        this.service.HugeFactor = this.model.Plus2;
        this.service.LargeFactor = this.model.Plus1;
        this.service.SmallFactor = this.model.Less1;
        this.service.TinyFactor = this.model.Less2;
        this.service.MicroFactor = this.model.Less3;
        this.service.FontFamily = this.model.SelectedFontFamilyName;
    }

    /// <summary>
    ///     Transfers settings from the service to the model.
    /// </summary>
    private void SetValues()
    {
        logger.Debug("Entered method.");

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