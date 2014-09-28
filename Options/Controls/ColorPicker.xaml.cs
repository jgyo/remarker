// ***********************************************************************
// Assembly         : ColorLibrary
// Solution         : ColorLibrary
// File name        : ColorPicker.xaml.cs
// Author           : Gil Yoder
// Created          : 09 08,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 08, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Options.Controls
{
#region Imports

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#endregion

/// <summary>
///     Interaction logic for UserControl1.xaml
/// </summary>
public partial class ColorPicker : UserControl
{
    #region Static Fields

    public static readonly DependencyProperty BlueValueProperty =
        DependencyProperty.Register(
            "BlueValue",
            typeof(double),
            typeof(ColorPicker),
            new FrameworkPropertyMetadata(
                default(double),
                OnColorValuePropertyChanged,
                OnCoerceValueCallback));

    public static readonly DependencyProperty ColorSwathProperty =
        DependencyProperty.Register(
            "ColorSwath",
            typeof(Color),
            typeof(ColorPicker),
            new FrameworkPropertyMetadata(Colors.Coral,
                                          OnColorSwathPropertyChanged));

    public static readonly DependencyProperty GreenValueProperty =
        DependencyProperty.Register(
            "GreenValue",
            typeof(double),
            typeof(ColorPicker),
            new FrameworkPropertyMetadata(
                default(double),
                OnColorValuePropertyChanged,
                OnCoerceValueCallback));

    public static readonly DependencyProperty RedValueProperty =
        DependencyProperty.Register(
            "RedValue",
            typeof(double),
            typeof(ColorPicker),
            new FrameworkPropertyMetadata(
                default(double),
                OnColorValuePropertyChanged,
                OnCoerceValueCallback));

    #endregion

    #region Constructors and Destructors

    public ColorPicker()
    {
        this.InitializeComponent();
    }

    #endregion

    #region Public Properties

    public double BlueValue
    {
        get
        {
            return (double)this.GetValue(BlueValueProperty);
        }
        set
        {
            this.SetValue(BlueValueProperty, value);
        }
    }

    public Color ColorSwath
    {
        get
        {
            return (Color)this.GetValue(ColorSwathProperty);
        }
        set
        {
            this.SetValue(ColorSwathProperty, value);
        }
    }

    public double GreenValue
    {
        get
        {
            return (double)this.GetValue(GreenValueProperty);
        }
        set
        {
            this.SetValue(GreenValueProperty, value);
        }
    }

    public double RedValue
    {
        get
        {
            return (double)this.GetValue(RedValueProperty);
        }
        set
        {
            this.SetValue(RedValueProperty, value);
        }
    }

    #endregion

    #region Methods

    private static object OnCoerceValueCallback(DependencyObject d,
            object basevalue)
    {
        var value = (double)basevalue;
        return Math.Min(255d, Math.Max(0d, value));
    }

    private static void OnColorSwathPropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        var colorPicker = d as ColorPicker;
        if (colorPicker == null || !(e.NewValue is Color))
        {
            return;
        }

        colorPicker.Dispatcher.Invoke(
            () =>
        {
            colorPicker.SetCurrentValue(RedValueProperty, (double)
                                        (((Color)e.NewValue).ScR * 255f));
            colorPicker.SetCurrentValue(GreenValueProperty, (double)
                                        (((Color)e.NewValue).ScG * 255f));
            colorPicker.SetCurrentValue(BlueValueProperty, (double)
                                        (((Color)e.NewValue).ScB * 255f));
        });
    }

    private static void OnColorValuePropertyChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        var colorPicker = d as ColorPicker;
        if (colorPicker == null)
        {
            return;
        }

        var color = new Color
        {
            ScA = 1f,
            ScR = (float)(colorPicker.RedValue / 255d),
            ScG = (float)(colorPicker.GreenValue / 255d),
            ScB = (float)(colorPicker.BlueValue / 255d)
        };

        colorPicker.Dispatcher.Invoke(
            () => colorPicker.SetCurrentValue(ColorSwathProperty, color));
    }

    #endregion
}
}