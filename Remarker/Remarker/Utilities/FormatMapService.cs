// ***********************************************************************
// Assembly         : Options.Package
// Solution         : YoderZone.Com.Extensions
// File name        : FormatMapService.cs
// Author           : Gil Yoder
// Created          : 09 03,  2014
//
// Last Modified By : Gil Yoder
// Last Modified On : 09 10, 2014
// ***********************************************************************

namespace YoderZone.Extensions.OptionsPackage.Remarker.Utilities
{
#region Imports

using System;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;

using YoderZone.Extensions.OptionsPackage.Remarker.FormatDefinitions;

#endregion

/// <summary>
///     A format map service.
/// </summary>
/// <seealso cref="T:System.IDisposable" />
internal sealed class FormatMapService : IDisposable
{
    #region Fields

    private readonly RemarkerSettings settings = RemarkerSettings.Default;

    /// <summary>
    ///     The format map.
    /// </summary>
    private IClassificationFormatMap formatMap;

    /// <summary>
    ///     The text view.
    /// </summary>
    private ITextView textView;

    /// <summary>
    ///     The type registry.
    /// </summary>
    private IClassificationTypeRegistryService typeRegistry;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///     Initializes a new instance of the
    ///     YoderZone.Extensions.Utilities.FormatMapService
    ///     class.
    /// </summary>
    /// <param name="textView" type="ITextView">
    ///     The text view.
    /// </param>
    /// <param name="formatMap" type="IClassificationFormatMap">
    ///     The format map.
    /// </param>
    /// <param name="typeRegistry" type="IClassificationTypeRegistryService">
    ///     The type registry.
    /// </param>
    public FormatMapService(
        ITextView textView,
        IClassificationFormatMap formatMap,
        IClassificationTypeRegistryService typeRegistry)
    {
        //+ FormatMapService Constructor

        this.textView = textView;
        this.formatMap = formatMap;
        this.typeRegistry = typeRegistry;
        this.UpdateFormatDefinitions();
        textView.GotAggregateFocus += this.OnViewGotAggregateFocus;
        this.formatMap.ClassificationFormatMappingChanged +=
            this.FormatMapChanged;
        textView.Closed += this.OnViewClosed;
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or
    ///     resetting
    ///     unmanaged resources.
    /// </summary>
    /// <seealso cref="M:System.IDisposable.Dispose()" />
    public void Dispose()
    {
        if (this.textView != null)
        {
            this.textView.Closed -= this.OnViewClosed;
            this.textView.GotAggregateFocus -= this.OnViewGotAggregateFocus;
            this.textView = null;
        }
        if (this.formatMap != null)
        {
            this.formatMap.ClassificationFormatMappingChanged -=
                this.FormatMapChanged;
            this.formatMap = null;
        }
        this.typeRegistry = null;
    }

    #endregion

    #region Methods

    internal void UpdateFormatDefinitions()
    {
        //! This method is called by the constructor, and two event handlers:
        //! FormatMapChanged, and OnViewGotAggregateFocus. The latter
        //! makes the call only once.

        if (this.formatMap == null || this.textView != null &&
                this.textView.IsClosed)
        {
            return;
        }

        bool isReentry = this.formatMap.IsInBatchUpdate;

        try
        {
            if (!isReentry)
            {
                this.formatMap.BeginBatchUpdate();
            }

            foreach (var typeString in Definitions.ClassificationTypeStrings)
            {
                if (typeString.StartsWith("Task", StringComparison.Ordinal))
                {
                    IClassificationType taskType =
                        this.typeRegistry.GetClassificationType(typeString);

                    int index = 0;
                    switch (typeString)
                    {
                        case Definitions.Task01:
                            index = 1;
                            break;
                        case Definitions.Task02:
                            index = 2;
                            break;
                        case Definitions.Task03:
                            index = 3;
                            break;
                        case Definitions.Task04:
                            index = 4;
                            break;
                        case Definitions.Task05:
                            index = 5;
                            break;
                        case Definitions.Task06:
                            index = 6;
                            break;
                        case Definitions.Task07:
                            index = 7;
                            break;
                        case Definitions.Task08:
                            index = 8;
                            break;
                        case Definitions.Task09:
                            index = 9;
                            break;
                        case Definitions.Task10:
                            index = 10;
                            break;
                    }

                    this.UpdateTaskAppearance(
                        taskType,
                        this.settings.ColorIndex[index],
                        this.settings.FontFamilyIndex[index],
                        this.settings.IsBoldIndex[index]);

                    return;
                }

                var remarkSize = RemarkSize.Regular;

                if (typeString.EndsWith("04em", StringComparison.OrdinalIgnoreCase))
                {
                    remarkSize = RemarkSize.Micro; /*emSizeFactor = 0.55d;*/
                }
                else if (typeString.EndsWith("06em", StringComparison.OrdinalIgnoreCase))
                {
                    remarkSize = RemarkSize.Tiny; /*emSizeFactor = 0.70d;*/
                }
                else if (typeString.EndsWith("08em", StringComparison.OrdinalIgnoreCase))
                {
                    remarkSize = RemarkSize.Small; /*emSizeFactor = 0.85d;*/
                }
                else if (typeString.EndsWith("15em", StringComparison.OrdinalIgnoreCase))
                {
                    remarkSize = RemarkSize.Large; /*emSizeFactor = 1.25d;*/
                }
                else if (typeString.EndsWith("20em", StringComparison.OrdinalIgnoreCase))
                {
                    remarkSize = RemarkSize.Huge; /*emSizeFactor = 1.5d;*/
                }
                else if (typeString.EndsWith("25em", StringComparison.OrdinalIgnoreCase))
                {
                    remarkSize = RemarkSize.Giagantic; /*emSizeFactor = 2.0d;*/
                }

                IClassificationType classificationType =
                    this.typeRegistry.GetClassificationType(typeString);

                this.UpdateCommentFontSize(classificationType, remarkSize);
            }
        }
        finally
        {
            if (!isReentry && this.formatMap.IsInBatchUpdate)
            {
                this.formatMap.EndBatchUpdate();
            }
        }
    }

    /// <summary>
    ///     Format map changed.
    /// </summary>
    /// <param name="sender" type="object">
    ///     Source of the event.
    /// </param>
    /// <param name="e" type="EventArgs">
    ///     Event information.
    /// </param>
    private void FormatMapChanged(object sender, EventArgs e)
    {
        this.UpdateFormatDefinitions();
    }

    /// <summary>
    ///     Event handler. Called by view for closed events.
    /// </summary>
    /// <param name="sender" type="object">
    ///     Source of the event.
    /// </param>
    /// <param name="e" type="EventArgs">
    ///     Event information.
    /// </param>
    private void OnViewClosed(object sender, EventArgs e)
    {
        var view = sender as ITextView;
        if (view != null)
        {
            view.Closed -= this.OnViewClosed;
        }

        this.Dispose();
    }

    private void OnViewGotAggregateFocus(object sender, EventArgs e)
    {
        var view = sender as ITextView;
        if (view != null)
        {
            view.GotAggregateFocus -= this.OnViewGotAggregateFocus;
        }

        this.UpdateFormatDefinitions();
    }

    //! Updates a comment's configuration definition.

    private void UpdateCommentFontSize(IClassificationType formatType,
                                       RemarkSize remarkSize)
    {
        IClassificationType textType =
            this.typeRegistry.GetClassificationType("text");
        TextFormattingRunProperties textProperties =
            this.formatMap.GetTextProperties(textType);

        if (textProperties.FontRenderingEmSizeEmpty)
        {
            // No hope.
            return;
        }

        var emSize = (float)textProperties.FontRenderingEmSize;
        this.settings.EmSize = emSize;
        float fontSize;
        switch (remarkSize)
        {
            case RemarkSize.Micro:
                fontSize = this.settings.MicroFontSize;
                break;
            case RemarkSize.Tiny:
                fontSize = this.settings.TinyFontSize;
                break;
            case RemarkSize.Small:
                fontSize = this.settings.SmallFontSize;
                break;
            case RemarkSize.Large:
                fontSize = this.settings.LargeFontSize;
                break;
            case RemarkSize.Huge:
                fontSize = this.settings.HugeFontSize;
                break;
            case RemarkSize.Giagantic:
                fontSize = this.settings.GiaganticFontSize;
                break;
            case RemarkSize.Regular:
                fontSize = emSize;
                break;
            default:
                throw new ArgumentOutOfRangeException("remarkSize");
        }

        TextFormattingRunProperties typeProperties =
            this.formatMap.GetTextProperties(
                formatType);

        typeProperties = typeProperties.SetFontRenderingEmSize(fontSize);
        string fontFamilyName = this.settings.FontFamily;

        if (fontFamilyName != null)
        {
            typeProperties = typeProperties.SetTypeface(new Typeface(fontFamilyName));
        }

        this.formatMap.SetTextProperties(formatType, typeProperties);
    }

    //! Updates a Task configuration definition.

    private void UpdateTaskAppearance(
        IClassificationType taskType,
        Color color,
        string font,
        bool isBold)
    {
        TextFormattingRunProperties textProperties =
            this.formatMap.GetTextProperties(taskType);
        textProperties = textProperties.SetTypeface(new Typeface(font));
        textProperties = textProperties.SetForegroundBrush(new SolidColorBrush(
                             color));
        textProperties.SetBold(isBold);
        this.formatMap.SetTextProperties(taskType, textProperties);
    }

    #endregion
}

internal enum RemarkSize
{
    Regular = 0,

    Micro,

    Tiny,

    Small,

    Large,

    Huge,

    Giagantic
}
}