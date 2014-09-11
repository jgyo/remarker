// ***********************************************************************
// Assembly         : Options.Dialog
// Solution         : YoderZone.Com.Extensions
// File name        : TextBoxFix.cs
// Author           : Gil Yoder
// Created          : 09 06,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 06, 2014
// ***********************************************************************

namespace YoderZone.Extensions.OptionsDialog.Controls
{
#region Imports

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

#endregion

/// <summary>
///     A text box fix.
/// </summary>
/// <seealso cref="T:System.Windows.Controls.TextBox" />
public class TextBoxFix : TextBox
{
    #region Constants

    /// <summary>
    ///     The dlgc hassetsel.
    /// </summary>
    private const UInt32 DLGC_HASSETSEL = 0x0008;

    /// <summary>
    ///     The dlgc wantallkeys.
    /// </summary>
    private const UInt32 DLGC_WANTALLKEYS = 0x0004;

    /// <summary>
    ///     The dlgc wantarrows.
    /// </summary>
    private const UInt32 DLGC_WANTARROWS = 0x0001;

    /// <summary>
    ///     The dlgc wantchars.
    /// </summary>
    private const UInt32 DLGC_WANTCHARS = 0x0080;

    /// <summary>
    ///     The dlgc wanttab.
    /// </summary>
    private const UInt32 DLGC_WANTTAB = 0x0002;

    /// <summary>
    ///     The windows message getdlgcode.
    /// </summary>
    private const UInt32 WM_GETDLGCODE = 0x0087;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the
    ///     YoderZone.Extensions.OptionsDialog.Controls.TextBoxFix class.
    /// </summary>
    public TextBoxFix()
    {
        this.Loaded += this.TextBoxFix_Loaded;
    }

    #endregion

    #region Methods

    /// <summary>
    ///     Child hwnd source hook.
    /// </summary>
    /// <param name="hwnd" type="IntPtr">
    ///     The.
    /// </param>
    /// <param name="msg" type="int">
    ///     The message.
    /// </param>
    /// <param name="wParam" type="IntPtr">
    ///     The parameter.
    /// </param>
    /// <param name="lParam" type="IntPtr">
    ///     The parameter.
    /// </param>
    /// <param name="handled" type="ref bool">
    ///     The handled.
    /// </param>
    /// <returns>
    ///     An IntPtr.
    /// </returns>
    private IntPtr ChildHwndSourceHook(
        IntPtr hwnd,
        int msg,
        IntPtr wParam,
        IntPtr lParam,
        ref bool handled)
    {
        if (msg == WM_GETDLGCODE)
        {
            handled = true;
            return new IntPtr(DLGC_WANTCHARS | DLGC_WANTARROWS | DLGC_HASSETSEL);
        }
        return IntPtr.Zero;
    }

    /// <summary>
    ///     Event handler. Called by TextBoxFix for loaded events.
    /// </summary>
    /// <param name="sender" type="object">
    ///     Source of the event.
    /// </param>
    /// <param name="e" type="RoutedEventArgs">
    ///     Routed event information.
    /// </param>
    private void TextBoxFix_Loaded(object sender, RoutedEventArgs e)
    {
        var s = PresentationSource.FromVisual(this) as HwndSource;
        if (s != null)
        {
            s.AddHook(this.ChildHwndSourceHook);
        }
    }

    #endregion
}
}