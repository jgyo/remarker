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

namespace YoderZone.Extensions.Remarker.Remarker.Utilities
{
#region Imports

using System;
using System.Windows.Media;

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;

using YoderZone.Extensions.OptionsPackage.Remarker.FormatDefinitions;
using YoderZone.Extensions.OptionsPackage.Remarker.Utilities;
using YoderZone.Extensions.Remarker.Remarker.Service;

#endregion

/// <summary>
///     A format map service.
/// </summary>
/// <seealso cref="T:System.IDisposable" />
internal sealed class FormatMapService : IDisposable
{
    #region Fields

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

    private readonly RemarkerService service;

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
    /// <param name="service"></param>
    /// <param name="formatMap" type="IClassificationFormatMap">
    ///     The format map.
    /// </param>
    /// <param name="typeRegistry" type="IClassificationTypeRegistryService">
    ///     The type registry.
    /// </param>
    public FormatMapService(ITextView textView, RemarkerService service,
                            IClassificationFormatMap formatMap,
                            IClassificationTypeRegistryService typeRegistry)
    {
        //+ FormatMapService Constructor

        this.textView = textView;
        this.formatMap = formatMap;
        this.service = service;
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
                //+ Handle Task classifications
                if (typeString.StartsWith("Task", StringComparison.Ordinal))
                {
                    IClassificationType taskType =
                        this.typeRegistry.GetClassificationType(typeString);

                    int index = 0;
                    switch (typeString)
                    {
                        case Definitions.Task01:
                            index = 0;
                            break;
                        case Definitions.Task02:
                            index = 1;
                            break;
                        case Definitions.Task03:
                            index = 2;
                            break;
                        case Definitions.Task04:
                            index = 3;
                            break;
                        case Definitions.Task05:
                            index = 4;
                            break;
                        case Definitions.Task06:
                            index = 5;
                            break;
                        case Definitions.Task07:
                            index = 6;
                            break;
                        case Definitions.Task08:
                            index = 7;
                            break;
                        case Definitions.Task09:
                            index = 8;
                            break;
                        case Definitions.Task10:
                            index = 9;
                            break;
                    }

                    this.UpdateTaskAppearance(
                        taskType,
                        service.ColorIndex[index],
                        service.FontFamilyIndex[index],
                        service.IsBoldIndex[index]);

                    continue;
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
                    remarkSize = RemarkSize.Gigantic; /*emSizeFactor = 2.0d;*/
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
        // Get the default text classification.
        var textType = this.typeRegistry.GetClassificationType("text");
        var textProperties = this.formatMap.GetTextProperties(textType);

        // We must have the em size. Otherwise we may as well give up.
        if (textProperties.FontRenderingEmSizeEmpty)
        {
            // No hope.
            return;
        }

        // Calculate the font size based on the em size times the factor.
        var emSize = (float)textProperties.FontRenderingEmSize;
        service.EmSize = emSize;
        float fontSize;
        switch (remarkSize)
        {
            case RemarkSize.Micro:
                fontSize = service.MicroFontSize;
                break;
            case RemarkSize.Tiny:
                fontSize = service.TinyFontSize;
                break;
            case RemarkSize.Small:
                fontSize = service.SmallFontSize;
                break;
            case RemarkSize.Large:
                fontSize = service.LargeFontSize;
                break;
            case RemarkSize.Huge:
                fontSize = service.HugeFontSize;
                break;
            case RemarkSize.Gigantic:
                fontSize = service.GiganticFontSize;
                break;
            case RemarkSize.Regular:
                fontSize = emSize;
                break;
            default:
                throw new ArgumentOutOfRangeException("remarkSize");
        }

        var typeProperties = this.formatMap.GetTextProperties(
                                 formatType);

        // Update the font size.
        typeProperties = typeProperties.SetFontRenderingEmSize(fontSize);
        string fontFamilyName = service.FontFamily;

        if (fontFamilyName != null)
        {
            // Update the font type.
            typeProperties = typeProperties.SetTypeface(new Typeface(fontFamilyName));
        }

        // Save the changes to the classification type.
        this.formatMap.SetTextProperties(formatType, typeProperties);
    }

    //! Updates a Task configuration definition.

    private void UpdateTaskAppearance(
        IClassificationType taskType,
        string color,
        string font,
        bool isBold)
    {
        // Get the task's current settings for a basis (mainly the size).
        var textProperties = this.formatMap.GetTextProperties(taskType);

        // Update the task's typeface property.
        textProperties = textProperties.SetTypeface(new Typeface(font));

        // Update the task's color property.
        textProperties = textProperties.SetForegroundBrush(new SolidColorBrush(
                             color.ConvertStringToWpfColor()));

        // Update the task's weight property.
        textProperties = textProperties.SetBold(isBold);
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

    Gigantic
}
}