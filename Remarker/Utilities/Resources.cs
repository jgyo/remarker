// ***********************************************************************
// Assembly         : Remarker
// Solution         : CommentPlus
// File name        : Resources.cs
// Author           : Gil Yoder
// Created          : 08 29,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 08 29, 2014
// ***********************************************************************

namespace YoderZone.Extensions.Remarker.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Windows.Media;

    using Microsoft.VisualStudio.Text.Tagging;

    using Color = System.Drawing.Color;

    /// <summary>
/// A resources.
/// </summary>
public static class Resources
{

    #region Static Fields

    public static readonly List<ITagSpan<ClassificationTag>> EmptyTags = new
    List<ITagSpan<ClassificationTag>>();

    public static string ConvertColorToString(this Color color)
    {

        var colorString = string.Format("{0:x2}{1:x2}{2:x2}", color.R, color.G,
                                        color.B);
        return colorString;
    }

    public static string ConvertWpfColorToString(this
            System.Windows.Media.Color color)
    {

        var colorString = string.Format("{0:x2}{1:x2}{2:x2}", color.R, color.G,
                                        color.B);
        return colorString;
    }

    public static System.Windows.Media.Color ConvertStringToWpfColor(this string colorString)
    {

        var result = Colors.Black;
        if (string.IsNullOrEmpty(colorString))
        {
            return result;
        }

        try
        {
            string redField = colorString.Substring(0, 2);
            string greenField = colorString.Substring(2, 2);
            string blueField = colorString.Substring(4, 2);

            byte redByte = redField.ConvertColorComponentToByte();
            var greenByte = greenField.ConvertColorComponentToByte();
            var blueByte = blueField.ConvertColorComponentToByte();

            result = System.Windows.Media.Color.FromArgb(255, redByte, greenByte, blueByte);
        }
        catch (Exception)
        {
        }

        return result;
    }

    public static Color ConvertStringToColor(
        this string colorString)
    {

        var result = Color.Black;
        if (string.IsNullOrEmpty(colorString))
        {
            return result;
        }

        try
        {
            string redField = colorString.Substring(0, 2);
            string greenField = colorString.Substring(2, 2);
            string blueField = colorString.Substring(4, 2);

            byte redByte = redField.ConvertColorComponentToByte();
            var greenByte = greenField.ConvertColorComponentToByte();
            var blueByte = blueField.ConvertColorComponentToByte();

            result = Color.FromArgb(redByte, greenByte, blueByte);
        }
        catch (Exception)
        {
        }

        return result;
    }

    public static byte ConvertColorComponentToByte(this string colorComponent)
    {
        Contract.Requires(colorComponent != null);
        Contract.Requires(colorComponent.Length ==
                2);

        colorComponent = colorComponent.ToLower();
        return (byte)(colorComponent[0].ConvertHexCharToDecimal() * 16 +
                      colorComponent[1].ConvertHexCharToDecimal());

    }

    public static byte ConvertHexCharToDecimal(this char hexChar)
    {

        switch (hexChar)
        {
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                return (byte)(hexChar - '0');
            case 'a':
            case 'b':
            case 'c':
            case 'd':
            case 'e':
            case 'f':
                return (byte)(hexChar - 'a' + 10);
            default:
                throw new ArgumentException("hexChar");
        }
    }

    /// <summary>
    /// The bug color.
    /// </summary>
    public static readonly System.Windows.Media.Color BugColor = Colors.Red;

    /// <summary>
    /// The hack color.
    /// </summary>
    public static readonly System.Windows.Media.Color HackColor = Colors.Chocolate;

    /// <summary>
    /// The normal comment color.
    /// </summary>
    public static readonly System.Windows.Media.Color NormalCommentColor = Colors.Green;

    /// <summary>
    /// The normal important color.
    /// </summary>
    public static readonly System.Windows.Media.Color NormalImportantColor = Colors.Red;

    /// <summary>
    /// The normal question color.
    /// </summary>
    public static readonly System.Windows.Media.Color NormalQuestionColor = Colors.Blue;

    /// <summary>
    /// The normal strikeout color.
    /// </summary>
    public static readonly System.Windows.Media.Color NormalStrikeoutColor = Colors.Gray;

    /// <summary>
    /// The note color.
    /// </summary>
    public static readonly System.Windows.Media.Color NoteColor = Colors.BlueViolet;

    /// <summary>
    /// The question color.
    /// </summary>
    public static readonly System.Windows.Media.Color QuestionColor = Colors.Green;

    /// <summary>
    /// The todo color.
    /// </summary>
    public static readonly System.Windows.Media.Color TodoColor = Colors.Blue;

    #endregion
}
}