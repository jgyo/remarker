// ***********************************************************************
// Assembly         : Options
// Solution         : YoderZone.Com.Extensions
// File name        : FontAttributes.cs
// Author           : Gil Yoder
// Created          : 09 01,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 01, 2014
// ***********************************************************************

namespace Options
{
#region Imports

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;

#endregion

/// <summary>
///     A font attributes.
/// </summary>
public class FontAttributes
{
    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the Options.FontAttributes class.
    /// </summary>
    /// <param name="fontFamily">
    ///     The font family.
    /// </param>
    /// <param name="graphics" type="Graphics">
    ///     The graphics.
    /// </param>
    public FontAttributes(FontFamily fontFamily, Graphics graphics)
    {
        this.HasBoldStyle = fontFamily.IsStyleAvailable(FontStyle.Bold);
        this.HasRegularStyle = fontFamily.IsStyleAvailable(FontStyle.Regular);
        this.HasStrikeoutStyle = fontFamily.IsStyleAvailable(FontStyle.Strikeout);
        this.HasItalicsStyle = fontFamily.IsStyleAvailable(FontStyle.Italic);
        this.Name = fontFamily.Name;
        this.FontFamily = fontFamily;
        this.CheckIfFixed(graphics);
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///     Gets the font family.
    /// </summary>
    /// <value>
    ///     The font family.
    /// </value>
    [Bindable(BindableSupport.No)]
    public FontFamily FontFamily { get; private set; }

    /// <summary>
    ///     Gets a value indicating whether this Options.FontAttributes has bold style.
    /// </summary>
    /// <value>
    ///     true if this Options.FontAttributes has bold style, false if not.
    /// </value>
    [Bindable(BindableSupport.No)]
    public bool HasBoldStyle { get; private set; }

    /// <summary>
    ///     Gets a value indicating whether this Options.FontAttributes has italics
    ///     style.
    /// </summary>
    /// <value>
    ///     true if this Options.FontAttributes has italics style, false if not.
    /// </value>
    [Bindable(BindableSupport.No)]
    public bool HasItalicsStyle { get; private set; }

    /// <summary>
    ///     Gets a value indicating whether this Options.FontAttributes has regular
    ///     style.
    /// </summary>
    /// <value>
    ///     true if this Options.FontAttributes has regular style, false if not.
    /// </value>
    [Bindable(BindableSupport.No)]
    public bool HasRegularStyle { get; private set; }

    /// <summary>
    ///     Gets a value indicating whether this Options.FontAttributes has strikeout
    ///     style.
    /// </summary>
    /// <value>
    ///     true if this Options.FontAttributes has strikeout style, false if not.
    /// </value>
    [Bindable(BindableSupport.No)]
    public bool HasStrikeoutStyle { get; private set; }

    /// <summary>
    ///     Gets the width of the is fixed.
    /// </summary>
    /// <value>
    ///     The width of the is fixed.
    /// </value>
    [Bindable(BindableSupport.No)]
    public bool? IsFixedWidth { get; private set; }

    /// <summary>
    ///     Gets the name.
    /// </summary>
    /// <value>
    ///     The name.
    /// </value>
    public string Name { get; private set; }

    #endregion

    #region Methods

    /// <summary>
    ///     Check if fixed.
    /// </summary>
    /// <param name="graphics" type="Graphics">
    ///     The graphics.
    /// </param>
    private void CheckIfFixed(Graphics graphics)
    {
        if (graphics == null)
        {
            return;
        }

        var charSizes = new[]
        {
            'i',
            'a',
            'Z',
            '%',
            '#',
            'a',
            'B',
            'l',
            'm',
            ',',
            '.'
        };

        var font = new Font(this.FontFamily, 12);
        float charWidth = graphics.MeasureString("I", font)
                          .Width;
        this.IsFixedWidth =
            charSizes.All(
                c =>
                Math.Abs(
                    graphics.MeasureString(c.ToString(CultureInfo.InvariantCulture), font)
                    .Width - charWidth) < 0.0000001);
    }

    #endregion
}
}