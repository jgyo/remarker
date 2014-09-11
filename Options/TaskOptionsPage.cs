// ***********************************************************************
// Assembly         : Options.Dialog
// Solution         : YoderZone.Com.Extensions
// File name        : TaskOptionsPage.cs
// Author           : Gil Yoder
// Created          : 09 04,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 05, 2014
// ***********************************************************************

namespace YoderZone.Extensions.OptionsDialog
{
#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

using YoderZone.Extensions.OptionsDialog.ViewModel;

#endregion

/// <summary>
///     A task options page.
/// </summary>
/// <seealso cref="T:System.Windows.Forms.UserControl" />
public partial class TaskOptionsPage : UserControl
{
    #region Static Fields

    /// <summary>
    ///     The default options page.
    /// </summary>
    private static TaskOptionsPage defaultOptionsPage;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the
    ///     YoderZone.Extensions.OptionsDialog.TaskOptionsPage
    ///     class.
    /// </summary>
    public TaskOptionsPage()
    {
        this.InitializeComponent();

        var installedFontCollection = new InstalledFontCollection();
        FontFamily[] fontFamilyArray = installedFontCollection.Families;

        var fontFamilies = new List<string>(fontFamilyArray.Length);
        using (Graphics graphics = this.CreateGraphics())
        {
            fontFamilies.AddRange(fontFamilyArray.Select(t => t.Name));

            graphics.Dispose();
        }

        ViewModel.TaskOptions.DefaultOptions.FontNames = fontFamilies;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///     Gets the default task options page.
    /// </summary>
    /// <value>
    ///     The default task options page.
    /// </value>
    public static TaskOptionsPage DefaultTaskOptionsPage
    {
        get
        {
            return defaultOptionsPage ?? (defaultOptionsPage = new TaskOptionsPage());
        }
    }

    #endregion

    public bool CanDeactivate()
    {
        if (ViewModel.TaskOptions.DefaultOptions.IsInEditMode)
        {
            (elementHost1.Child as TaskOptions).Confirm();
        }

        return true;
    }
}
}