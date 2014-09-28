// ***********************************************************************
// Assembly         : Options.Dialog
// Solution         : YoderZone.Com.Extensions
// File name        : CommentOptionsPage.cs
// Author           : Gil Yoder
// Created          : 09 01,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 06, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Options
{
#region Imports

using System.Windows.Forms;

using YoderZone.Extensions.Options.ViewModel;

#endregion

/// <summary>
///     The options page.
/// </summary>
/// <seealso cref="T:System.Windows.Forms.UserControl" />
/// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged" />
public partial class CommentOptionsPage : UserControl
{
    private readonly CommentOptions model;

    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the Options.CommentOptionsPage class.
    /// </summary>
    public CommentOptionsPage(CommentOptions commentOptions)
    {
        this.model = commentOptions;
        this.InitializeComponent();
        this.commentOptions1.DataContext = commentOptions;
    }

    #endregion

    public bool CanDeactivate()
    {
        return this.model.Error == "";
    }
}
}