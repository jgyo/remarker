// ***********************************************************************
// Assembly         : Options.Dialog
// Solution         : YoderZone.Com.Extensions
// File name        : PopupColorPicker.xaml.cs
// Author           : Gil Yoder
// Created          : 09 08,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 08, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Options.Controls
{
#region Imports

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#endregion

/// <summary>
///     Interaction logic for PopupColorPicker.xaml
/// </summary>
public partial class PopupColorPicker : UserControl
{
    #region Static Fields

    public static readonly DependencyProperty ColorSwathProperty =
        DependencyProperty.Register(
            "ColorSwath",
            typeof(Color),
            typeof(PopupColorPicker),
            new PropertyMetadata(Colors.Coral));

    public static readonly DependencyProperty DisablePopupProperty =
        DependencyProperty.Register(
            "DisablePopup",
            typeof(bool),
            typeof(PopupColorPicker),
            new FrameworkPropertyMetadata(false));

    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(
            "IsOpen",
            typeof(bool),
            typeof(PopupColorPicker),
            new PropertyMetadata(default(bool)));

        #endregion

        #region Constructors and Destructors

        public PopupColorPicker() => this.InitializeComponent();

        #endregion

        #region Public Properties

        public Color ColorSwath
    {
            get => (Color)this.GetValue(ColorSwathProperty);
            set => this.SetValue(ColorSwathProperty, value);
        }

    public bool DisablePopup
    {
            get => (bool)this.GetValue(DisablePopupProperty);
            set => this.SetValue(DisablePopupProperty, value);
        }

    public bool IsOpen
    {
            get => (bool)this.GetValue(IsOpenProperty);
            set => this.SetValue(IsOpenProperty, value);
        }

    #endregion
}
}