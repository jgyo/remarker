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

namespace YoderZone.Extensions.Options
{
#region Imports

    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Linq;
    using System.Windows.Forms;

    using YoderZone.Extensions.Options.ViewModel;

    #endregion

/// <summary>
///     A task options page.
/// </summary>
/// <seealso cref="T:System.Windows.Forms.UserControl" />
public partial class TaskOptionsPage : UserControl
{
    #region Static Fields

    #endregion

    private readonly TaskOptionsControl control;

    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the
    ///     YoderZone.Extensions.OptionsDialog.TaskOptionsPage
    ///     class.
    /// </summary>
    public TaskOptionsPage(TaskOptions model)
    {

        this.InitializeComponent();
        this.control = this.taskOptionControl;
        this.control.DataContext = model;
    }

    #endregion

    public bool CanDeactivate()
    {

        return this.control.CanDeactivate;
    }
}
}