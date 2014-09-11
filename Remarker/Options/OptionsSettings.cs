// ***********************************************************************
// Assembly         : Remarker
// Solution         : YoderZone.Com.Extensions
// File name        : OptionsSettings.cs
// Author           : Gil Yoder
// Created          : 09 03,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 03, 2014
// ***********************************************************************

namespace YoderZone.Extensions.OptionsPackage.Options
{
#region Imports

using System;

#endregion

/// <summary>
///     The options settings.
/// </summary>
public class OptionsSettings
{
    #region Static Fields

    /// <summary>
    ///     The page.
    /// </summary>
    private static RemarkerGeneralOptionsPage page;

    #endregion

    #region Public Properties

    /// <summary>
    ///     Gets or sets the size of the em.
    /// </summary>
    /// <value>
    ///     The size of the em.
    /// </value>
    public static float? EmSize { get; set; }

    public static string FontFamilyName
    {
        get
        {
            return Page == null ? null : Page.FontFamily;
        }
    }

    /// <summary>
    ///     Gets the giagantic factor.
    /// </summary>
    /// <value>
    ///     The giagantic factor.
    /// </value>
    public static float? GiaganticFactor
    {
        get
        {
            return Page == null ? null : (float?)Page.GiaganticFactor;
        }
    }

    /// <summary>
    ///     Gets the size of the giagantic font.
    /// </summary>
    /// <value>
    ///     The size of the giagantic font.
    /// </value>
    public static float? GiaganticFontSize
    {
        get
        {
            if (EmSize == null || GiaganticFactor == null)
            {
                return null;
            }

            return EmSize * GiaganticFactor;
        }
    }

    /// <summary>
    ///     Gets the huge factor.
    /// </summary>
    /// <value>
    ///     The huge factor.
    /// </value>
    public static float? HugeFactor
    {
        get
        {
            return Page == null ? null : (float?)Page.HugeFactor;
        }
    }

    /// <summary>
    ///     Gets the size of the huge font.
    /// </summary>
    /// <value>
    ///     The size of the huge font.
    /// </value>
    public static float? HugeFontSize
    {
        get
        {
            if (EmSize == null || HugeFactor == null)
            {
                return null;
            }

            return EmSize * HugeFactor;
        }
    }

    /// <summary>
    ///     Gets the large factor.
    /// </summary>
    /// <value>
    ///     The large factor.
    /// </value>
    public static float? LargeFactor
    {
        get
        {
            return Page == null ? null : (float?)Page.LargeFactor;
        }
    }

    /// <summary>
    ///     Gets the size of the large font.
    /// </summary>
    /// <value>
    ///     The size of the large font.
    /// </value>
    public static float? LargeFontSize
    {
        get
        {
            if (EmSize == null || LargeFactor == null)
            {
                return null;
            }

            return EmSize * LargeFactor;
        }
    }

    /// <summary>
    ///     Gets the micro factor.
    /// </summary>
    /// <value>
    ///     The micro factor.
    /// </value>
    public static float? MicroFactor
    {
        get
        {
            return Page == null ? null : (float?)Page.MicroFactor;
        }
    }

    /// <summary>
    ///     Gets the size of the micro font.
    /// </summary>
    /// <value>
    ///     The size of the micro font.
    /// </value>
    public static float? MicroFontSize
    {
        get
        {
            if (EmSize == null || MicroFactor == null)
            {
                return null;
            }

            return EmSize * MicroFactor;
        }
    }

    /// <summary>
    ///     Gets or sets the page.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the requested operation is invalid.
    /// </exception>
    /// <value>
    ///     The page.
    /// </value>
    public static RemarkerGeneralOptionsPage Page
    {
        get
        {
            return page;
        }
        set
        {
            if (value == null)
            {
                throw new InvalidOperationException();
            }

            if (page != null && page != value)
            {
                throw new InvalidOperationException();
            }

            page = value;
        }
    }

    /// <summary>
    ///     Gets the small factor.
    /// </summary>
    /// <value>
    ///     The small factor.
    /// </value>
    public static float? SmallFactor
    {
        get
        {
            return Page == null ? null : (float?)Page.SmallFactor;
        }
    }

    /// <summary>
    ///     Gets the size of the small font.
    /// </summary>
    /// <value>
    ///     The size of the small font.
    /// </value>
    public static float? SmallFontSize
    {
        get
        {
            if (EmSize == null || SmallFactor == null)
            {
                return null;
            }

            return EmSize * SmallFactor;
        }
    }

    /// <summary>
    ///     Gets the tiny factor.
    /// </summary>
    /// <value>
    ///     The tiny factor.
    /// </value>
    public static float? TinyFactor
    {
        get
        {
            return Page == null ? null : (float?)Page.TinyFactor;
        }
    }

    /// <summary>
    ///     Gets the size of the tiny font.
    /// </summary>
    /// <value>
    ///     The size of the tiny font.
    /// </value>
    public static float? TinyFontSize
    {
        get
        {
            if (EmSize == null || TinyFactor == null)
            {
                return null;
            }

            return EmSize * TinyFactor;
        }
    }

    #endregion
}
}