// ***********************************************************************
// Assembly         : ColorLibrary
// Solution         : ColorLibrary
// File name        : ColorView.xaml.cs
// Author           : Gil Yoder
// Created          : 09 08,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 08, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Options.Controls.Elements
{
    #region Imports

    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    #endregion Imports

    /// <summary>
    ///     Interaction logic for ColorView.xaml
    /// </summary>
    public partial class ColorView : UserControl
    {
        #region Static Fields

        public static readonly DependencyProperty BlueValueProperty =
            DependencyProperty.Register(
                "BlueValue",
                typeof(double),
                typeof(ColorView),
                new FrameworkPropertyMetadata(
                    default(double),
                    OnColorValuePropertyChanged,
                    OnCoerceValueCallback));

        public static readonly DependencyProperty ColorSwathProperty =
            DependencyProperty.Register(
                "ColorSwath",
                typeof(Color),
                typeof(ColorView),
                new FrameworkPropertyMetadata(default(Color),
                                              OnColorSwathPropertyChanged));

        public static readonly DependencyProperty GreenValueProperty =
            DependencyProperty.Register(
                "GreenValue",
                typeof(double),
                typeof(ColorView),
                new FrameworkPropertyMetadata(
                    default(double),
                    OnColorValuePropertyChanged,
                    OnCoerceValueCallback));

        public static readonly DependencyProperty RedValueProperty =
            DependencyProperty.Register(
                "RedValue",
                typeof(double),
                typeof(ColorView),
                new FrameworkPropertyMetadata(
                    default(double),
                    OnColorValuePropertyChanged,
                    OnCoerceValueCallback));

        #endregion Static Fields

        #region Constructors and Destructors

        public ColorView() => this.InitializeComponent();

        #endregion Constructors and Destructors

        #region Public Properties

        public double BlueValue
        {
            get => (double)this.GetValue(BlueValueProperty);
            set => this.SetValue(BlueValueProperty, value);
        }

        public Color ColorSwath
        {
            get => (Color)this.GetValue(ColorSwathProperty);
            set => this.SetValue(ColorSwathProperty, value);
        }

        public double GreenValue
        {
            get => (double)this.GetValue(GreenValueProperty);
            set => this.SetValue(GreenValueProperty, value);
        }

        public double RedValue
        {
            get => (double)this.GetValue(RedValueProperty);
            set => this.SetValue(RedValueProperty, value);
        }

        #endregion Public Properties

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
            var colorView = d as ColorView;
            if (colorView == null || !(e.NewValue is Color))
            {
                return;
            }

            colorView.Dispatcher.Invoke(
                () =>
            {
                colorView.Background = new SolidColorBrush((Color)e.NewValue);

                colorView.SetCurrentValue(RedValueProperty, (double)
                                          (((Color)e.NewValue).ScR * 255f));
                colorView.SetCurrentValue(GreenValueProperty, (double)
                                          (((Color)e.NewValue).ScG * 255f));
                colorView.SetCurrentValue(BlueValueProperty, (double)
                                          (((Color)e.NewValue).ScB * 255f));
            });
        }

        private static void OnColorValuePropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var colorView = d as ColorView;
            if (colorView == null)
            {
                return;
            }

            var color = new Color
            {
                ScA = 255f,
                ScR = (float)(colorView.RedValue / 255d),
                ScG = (float)(colorView.GreenValue / 255d),
                ScB = (float)(colorView.BlueValue / 255f)
            };

            colorView.Dispatcher.Invoke(() => colorView.SetCurrentValue(
                                            ColorSwathProperty, color));
        }

        #endregion Methods
    }
}