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

namespace YoderZone.Extensions.OptionsDialog
{
#region Imports

using System;
using System.Diagnostics;
using System.Windows.Forms;

#endregion

/// <summary>
///     The options page.
/// </summary>
/// <seealso cref="T:System.Windows.Forms.UserControl" />
/// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged" />
public partial class CommentOptionsPage : UserControl
{
    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the Options.CommentOptionsPage class.
    /// </summary>
    private CommentOptionsPage()
    {

        this.InitializeComponent();
    }

    static CommentOptionsPage()
    {
        DefaultCommentOptionsPage = new CommentOptionsPage();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///     Gets or sets options for controlling the comment.
    /// </summary>
    /// <value>
    ///     Options that control the comment.
    /// </value>
    public ViewModel.CommentOptions CommentOptions
    {
        get
        {
            return ViewModel.CommentOptions.DefaultOptions;
        }
    }

    public static CommentOptionsPage DefaultCommentOptionsPage { get; private set; }

    #endregion

    public bool CanDeactivate()
    {
        return this.CommentOptions.Error == "";
    }
}
}