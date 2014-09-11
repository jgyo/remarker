// ***********************************************************************
// Assembly         : Options.Package
// Solution         : YoderZone.Com.Extensions
// File name        : RemarkerGeneralOptionsPage.cs
// Author           : Gil Yoder
// Created          : 09 03,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 09, 2014
// ***********************************************************************

namespace YoderZone.Extensions.OptionsPackage.Options
{
#region Imports

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Microsoft.VisualStudio.Shell;

using YoderZone.Extensions.OptionsDialog;

using CommentOptions =
    YoderZone.Extensions.OptionsDialog.ViewModel.CommentOptions;

#endregion

/// <summary>
///     A remarker general options page.
/// </summary>
/// <seealso cref="T:Microsoft.VisualStudio.Shell.DialogPage" />
[ClassInterface(ClassInterfaceType.AutoDual)]
[ToolboxItem(false)]
[CLSCompliant(false)]
[ComVisible(true)]
public class RemarkerGeneralOptionsPage : DialogPage
{
    #region Fields

    private readonly RemarkerSettings settings = RemarkerSettings.Default;

    /// <summary>
    ///     The window.
    /// </summary>
    private readonly IWin32Window window;

    private CommentOptionsPage control;

    private CommentOptions model;

    #endregion

    #region Public Properties

    /// <summary>
    ///     Gets or sets the font family.
    /// </summary>
    /// <value>
    ///     The font family.
    /// </value>
    [Category("Comment Settings")]
    [DisplayName("Font Family")]
    public string FontFamily
    {
        get
        {
            return this.settings.FontFamily;
        }
        set
        {
            this.settings.FontFamily = value;
        }
    }

    /// <summary>
    ///     Gets or sets the gigantic factor.
    /// </summary>
    /// <value>
    ///     The gigantic factor.
    /// </value>
    [Category("Comment Settings")]
    [DisplayName("Gigantic Factor")]
    public float GiganticFactor
    {
        get
        {
            return this.settings.GiantFactor;
        }
        set
        {
            this.settings.GiantFactor = value;
        }
    }

    /// <summary>
    ///     Gets or sets the huge factor.
    /// </summary>
    /// <value>
    ///     The huge factor.
    /// </value>
    [Category("Comment Settings")]
    [DisplayName("Huge Factor")]
    public float HugeFactor
    {
        get
        {
            return this.settings.HugeFactor;
        }
        set
        {
            this.settings.HugeFactor = value;
        }
    }

    /// <summary>
    ///     Gets or sets the large factor.
    /// </summary>
    /// <value>
    ///     The large factor.
    /// </value>
    [Category("Comment Settings")]
    [DisplayName("Large Factor")]
    public float LargeFactor
    {
        get
        {
            return this.settings.LargeFactor;
        }
        set
        {
            this.settings.LargeFactor = value;
        }
    }

    /// <summary>
    ///     Gets or sets the micro factor.
    /// </summary>
    /// <value>
    ///     The micro factor.
    /// </value>
    [Category("Comment Settings")]
    [DisplayName("Micro Factor")]
    public float MicroFactor
    {
        get
        {
            return this.settings.MicroFactor;
        }
        set
        {
            this.settings.MicroFactor = value;
        }
    }

    /// <summary>
    ///     Gets or sets the small factor.
    /// </summary>
    /// <value>
    ///     The small factor.
    /// </value>
    [Category("Comment Settings")]
    [DisplayName("Small Factor")]
    public float SmallFactor
    {
        get
        {
            return this.settings.SmallFactor;
        }
        set
        {
            this.settings.SmallFactor = value;
        }
    }

    /// <summary>
    ///     Gets or sets the tiny factor.
    /// </summary>
    /// <value>
    ///     The tiny factor.
    /// </value>
    [Category("Comment Settings")]
    [DisplayName("Tiny Factor")]
    public float TinyFactor
    {
        get
        {
            return this.settings.TinyFactor;
        }
        set
        {
            this.settings.TinyFactor = value;
        }
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
            return this.control ?? (this.control =
                                        CommentOptionsPage.DefaultCommentOptionsPage);
        }
    }

    private CommentOptions Model
    {
        get
        {
            return this.model ?? (this.model = CommentOptions.DefaultOptions);
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
        this.FontFamily = "Verdana";
        this.GiganticFactor = 2.0f;
        this.HugeFactor = 1.667f;
        this.LargeFactor = 1.3333f;
        this.MicroFactor = 0.55f;
        this.SmallFactor = 0.85f;
        this.TinyFactor = 0.7f;

        this.settings.Save();
        this.SetValues();
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
        var page = this.window as CommentOptionsPage;
        if (page != null && page.IsDisposed == false)
        {
            page.Dispose();
        }

        base.Dispose(disposing);
    }

    /// <summary>
    ///     Handles Windows Activate messages from the Visual Studio environment.
    /// </summary>
    /// <param name="e">[in] Arguments to event handler.</param>
    protected override void OnActivate(CancelEventArgs e)
    {
        this.SetValues();
        base.OnActivate(e);
    }

    /// <summary>
    ///     Handles Deactivated messages from the Visual Studio environment.
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

    private void ApplyChanges()
    {
        if (this.control == null)
        {
            return;
        }

        this.GiganticFactor = this.model.Plus3;
        this.HugeFactor = this.model.Plus2;
        this.LargeFactor = this.model.Plus1;
        this.SmallFactor = this.model.Less1;
        this.TinyFactor = this.model.Less2;
        this.MicroFactor = this.model.Less3;
        this.FontFamily = this.model.SelectedFontFamilyName;

        this.settings.Save();
    }

    private void SetValues()
    {
        if (this.control == null)
        {
            return;
        }

        this.settings.Reload();

        this.Model.Plus3 = this.GiganticFactor;
        this.Model.Plus2 = this.HugeFactor;
        this.Model.Plus1 = this.LargeFactor;
        this.Model.Less1 = this.SmallFactor;
        this.Model.Less2 = this.TinyFactor;
        this.model.Less3 = this.MicroFactor;
        this.model.SelectedFontFamilyName = this.FontFamily;
    }

    #endregion
}
}