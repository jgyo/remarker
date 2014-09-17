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
using System.Windows.Forms.Integration;

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

    private TaskOptions model;

    private TaskOptionsControl control;

    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the
    ///     YoderZone.Extensions.OptionsDialog.TaskOptionsPage
    ///     class.
    /// </summary>
    public TaskOptionsPage(TaskOptions model)
    {
        this.model = model;

        this.InitializeComponent();
        control = this.taskOptionControl;
        control.DataContext = model;

        var installedFontCollection = new InstalledFontCollection();
        FontFamily[] fontFamilyArray = installedFontCollection.Families;

        var fontFamilies = new List<string>(fontFamilyArray.Length);
        using (Graphics graphics = this.CreateGraphics())
        {
            fontFamilies.AddRange(fontFamilyArray.Select(t => t.Name));

            graphics.Dispose();
        }

        model.FontNames = fontFamilies;
    }

    #endregion

    public bool CanDeactivate()
    {
        return control.CanDeactivate;
    }
}
}