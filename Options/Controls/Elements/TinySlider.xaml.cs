namespace YoderZone.Extensions.OptionsDialog.Controls.Elements
{
using System;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for TinySlider.xaml
/// </summary>
public partial class TinySlider : UserControl
{
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(double), typeof(TinySlider),
                                    new PropertyMetadata(default(double), null, CoerceValuePropertyCallback));

    private static object CoerceValuePropertyCallback(DependencyObject d,
            object basevalue)
    {
        var value = (double)basevalue;

        return Math.Min(255d, Math.Max(0d, value));
    }

    public TinySlider()
    {
        InitializeComponent();
    }

    public double Value
    {
        get
        {
            return (double)this.GetValue(ValueProperty);
        }
        set
        {
            this.SetValue(ValueProperty, value);
        }
    }
}
}
